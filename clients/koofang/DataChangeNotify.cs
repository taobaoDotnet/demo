using System.Collections.Generic;

namespace servicedemo.clients.koofang
{
    using client.sis2koofang.models.response;

    interface DataChangeNotify
    {
        /// <summary>
        /// 小区信息
        /// </summary>
        /// <returns>前10条失败请求结果</returns>
        IEnumerable<KoofangResponse> CommunityChange();

        /// <summary>
        /// 小区图片
        /// </summary>
        /// <returns>前10条失败请求结果</returns>
        IEnumerable<KoofangResponse> CommunityImgChange();

        /// <summary>
        /// 房源信息
        /// </summary>
        /// <returns>前10条失败请求结果</returns>
        IEnumerable<KoofangResponse> RealtyChange();

        /// <summary>
        /// 房源图片
        /// </summary>
        /// <returns>前10条失败请求结果</returns>
        IEnumerable<KoofangResponse> RealtyImgChange();
    }
}
