using Cotide.Portal.Controllers.ViewModels.User;

namespace Cotide.Portal.Controllers.ViewModels.OAuth
{
    /// <summary>
    /// OAuth登陆视图实体
    /// </summary>
    public class OauthLoginViewModel
    {
         
        /// <summary>
        /// 请求类型
        /// </summary>
        public string ResponseType { get; set; }

        /// <summary>
        /// 客户端标识
        /// </summary>
        public string  ClientId { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClentName { get; set; }

        /// <summary>
        /// 状态码 (防CSRF 攻击)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 回调页面地址
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 应用授权作用域
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 当前用户
        /// </summary>
        public UserViewModel UserViewModel { get; set; }
    }
}