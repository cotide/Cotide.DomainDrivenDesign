using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{ 
    /// <summary>
    /// 验证控件配置
    /// </summary>
    public class ValidateOption
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
        /// 正则表达式
        /// </summary>
        public string Regex { get; set; }

        /// <summary>
        /// 正则表达式不匹配描述
        /// </summary>
        public string RegexMsg { get; set; }

        /// <summary>
        /// Ajax验证 URL地址
        /// </summary>
        public string AjaxUrl { get; set; }

        /// <summary>
        /// Ajax处理 函数名
        /// </summary>
        public string AjaxFun { get; set; }

        /// <summary>
        /// 比较验证的对比对象ID
        /// </summary>
        public string CompareElem { get; set; }

        /// <summary>
        /// 比较验证消息
        /// </summary>
        public string CompareElemMsg { get; set; }

        /// <summary>
        /// 是否必须项 默认为true
        /// </summary>
        public bool IsEmpty { get; set; }

        /// <summary>
        /// 是否启动验证
        /// </summary>
        public bool Validator { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ValidateOption()
        {
            EmptyMsg = null;
            Description = null;
            FocusMsg = null;
            Min = null;
            Max = null;
            RangeMsg = null;
            Regex = null;
            RegexMsg = null;
            AjaxUrl = null;
            CompareElem = null;
            CompareElemMsg = null;
            IsEmpty = false;
            Validator = true;
        }
    }

}