using System.ComponentModel.DataAnnotations;

namespace servicedemo.models.dto.comm
{
    public class PageInfoRequest
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        [Range(1, 9999, ErrorMessage = "当前页数为1-999的整数")]
        public uint currentPage { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        [Range(1, 100, ErrorMessage = "每页显示条数为1-100的整数")]
        public byte pageSize { get; set; }
    }
}
