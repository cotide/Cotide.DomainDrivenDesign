using System;

namespace Cotide.Portal.Controllers.Core
{
    /// <summary>
    /// 接口响应类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WebOutputResult<T>
    {
         
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime OutPutDateTime { get; set; }
    }
}