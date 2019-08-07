namespace servicedemo.client.sis2koofang.models.request
{
    /// <summary>
    /// 数据改变通知
    /// </summary>
    public class DataChangeKoofangRequest
    {
        public long FalgId { get; set; }
        public string Area { get; set; }
        public long Id { get; set; }
        public string Action { get; set; }
    }
}
