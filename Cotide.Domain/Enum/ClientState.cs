using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Cotide.Domain.Enum
{
    /// <summary>
    /// 客户端状态
    /// </summary>
    public enum ClientState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal,
         
        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Pause, 

        /// <summary>
        /// 停止
        /// </summary>
        [Description("停止")]
        Stop
    }
}
