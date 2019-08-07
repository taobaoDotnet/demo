namespace servicedemo.models.dto.comm
{
    public class PageInfoResponse
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        public uint currentPage { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public uint pageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public uint totalPages { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public uint totalRows { get; set; }
    }
}
