using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    /// <summary>
    /// 密码框验证控件配置
    /// </summary>
    public class PassValidateOption
    {
        /// <summary>
        /// 为空描述
        /// </summary>
        public string EmptyMsg { get; set; }

        /// <summary>
        /// 默认描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 失去焦点提示
        /// </summary>
        public string FocusMsg { get; set; }

        /// <summary>
        /// 范围验证最少长度
        /// </summary>
        public int? Min { get; set; }

        /// <summary>
        /// 范围验证最大长度
        /// </summary>
        public int? Max { get; set; }

        /// <summary>
        /// 范围验证描述
        /// </summary>
        public string RangeMsg { get; set; }

        /// <summary>
        /// 比较验证的对比对象ID
        /// </summary>
        public string CompareElem { get; set; }

        /// <summary>
        /// 比较验证消息
        /// </summary>
        public string CompareElemMsg { get; set; }

        /// <summary>
        /// 是否必须项 默认为false
        /// </summary>
        public bool IsEmpty { get; set; }

        public PassValidateOption()
        {
            EmptyMsg = null;
            Description = null;
            FocusMsg = null;
            Min = null;
            Max = null;
            RangeMsg = null;
            CompareElem = null;
            CompareElemMsg = null;
            IsEmpty = false;
        }
    }
}