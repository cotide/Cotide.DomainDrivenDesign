
using System.ComponentModel;

namespace Cotide.Portal.Controllers.ViewModels
{
    /// <summary>
    /// 用户视图
    /// </summary>
    public class PagingViewModel
    {
        /// <summary>
        /// 第几页
        /// </summary>
        [DisplayName(@" 第几页")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示多少行
        /// </summary>
        [DisplayName(@"每页显示多少行")]
        public int PageSize { get; set; }
    }
}
