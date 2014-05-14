namespace Cotide.Portal.Controllers.Core
{
    /// <summary>
    /// 错误信息
    /// </summary>
    public class ErrorMsg
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public string ErrorCode { get; set; }
        

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorDesc { get; set; }

        /// <summary>
        /// 错误详细说明页面
        /// </summary>
        public string ErrorUrl { get; set; }
 
    }
}