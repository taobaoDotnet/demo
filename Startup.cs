using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;

namespace servicedemo
{
    using CSRedis;
    using Microsoft.OpenApi.Models;
    using middleware;
    using System;
    using System.IO;
    using System.Reflection;

    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        // 构造函数注入
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
                //options =>
                //{
                //    //MVC统一错误处理
                //     options.Filters.Add<MyExceptionHandler>();
                //}
             )
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            // 配置文件映射
            services.Configure<AppSettings>(Configuration);

            #region service injecting
            services.AddTransient<services.User, services.UserImpl>();
            #endregion

            #region redis helper,MS Distributed Cache
            // 初始化RedisHelper
            //var residConn = Configuration["ConnectionStrings:RedisConn"].ToString();
            //RedisHelper.Initialization(new CSRedisClient(residConn));

            var csredis = new CSRedisClient(Configuration.GetConnectionString("RedisConn"));
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
            //注册mvc分布式缓存(eg:把Session存到Redis,kube部署后多台轮询需要统一存储session)
            services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
            #endregion

            #region Register the Swagger generator, defining 1 or more Swagger documents
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            //app.UseHttpsRedirection();

            #region swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            #endregion


            #region gloabl exception
            //app.UseGloablExceptionMiddleware();
            app.UseMiddleware<ExceptionMiddleware>();
            #endregion

            app.UseMvc();
        }

    }
}
