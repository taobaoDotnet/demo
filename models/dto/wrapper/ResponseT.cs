namespace servicedemo.models.dto.wrapper
{
    using enums;

    public class ResponseT<TData>
    {
        /// <summary>
        /// 构造默认值
        /// </summary>
        public ResponseT()
        {
            code = CodeEnum.Success;
            msg = CodeEnum.Success.Description();
        }

        /// <summary>
        /// 返回代码
        /// </summary>
        public CodeEnum code { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string msg { get; set; }

 
        /// <summary>
        /// 通用返回数据
        /// </summary>
        public TData data { get; set; }

        /// <summary>
        /// 对象序列化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
