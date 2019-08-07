using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

/// <summary>
/// 统一异常处理
/// 参考:https://thecodebuzz.com/best-practices-for-handling-exception-in-net-core-2-1/   https://thecodebuzz.com/filters-in-net-core-best-practices/
/// 
/// 1.不能完全依赖:被统一异常处理拦截后，本次Request的整个任务就终止了，所以还是需要处理需要跳过的异常。
/// 2.消费其他服务(Client)的异常:无法拦截Client请求提交的数据，只能拦截Request的输入数据。
/// 3.链路追踪
/// 4.全链路监控(APM)：https://github.com/SkyAPM/SkyAPM-dotnet
/// </summary>
namespace servicedemo.middleware
{
    using servicedemo.models.dto.comm;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddleware(RequestDelegate next,ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex.ToString()}");
                await HandleGlobalExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            object requestData = null;
            if (context.Request.Method=="POST" && context.Request.Body.Length>0)
            {
                using (var reader = new System.IO.StreamReader(context.Request.Body, System.Text.Encoding.UTF8))
                {
                    context.Request.Body.Position = 0;
                    requestData = reader.ReadToEnd();
                }
            }
            else if(context.Request.Method == "GET")
            {
                requestData = string.Join("&",context.Request.Query.Select(q => $"{q.Key}={q.Value}"));
            }

            return context.Response.WriteAsync(
                new ErrorMessage
                {
                    code = context.Response.StatusCode,
                    requestMethod = context.Request.Method,
                    requestUrl = $"{context.Request.Scheme}://{context.Request.Host}/{context.Request.Path}/",
                    requestData = requestData,
#if DEBUG
                    msg = $"{exception.Message}:{exception.StackTrace}"
#else
                    //msg= "Something went wrong !Internal Server Error"
                    msg = $"{exception.Message}:{exception.StackTrace}"
#endif
                }.ToString()
            ); ;
        }

    }
}
