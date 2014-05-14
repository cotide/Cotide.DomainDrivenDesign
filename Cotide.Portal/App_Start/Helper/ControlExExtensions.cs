using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Cotide.Framework.Extensions;


namespace System.Web.Mvc
{
    /// <summary>
    /// HtmlHelper 扩展
    /// </summary>
    /// <remarks>
    ///     <para>    Creator：xcli</para>
    ///     <para>CreatedTime：2013/7/29 18:17:52</para>
    /// </remarks>
    public static class ControlExExtensions
    { 
        /// <summary>
        /// 验证脚本
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="formId">表单ID</param>
        /// <returns></returns>
        public static MvcHtmlString ValidationExSummary(this System.Web.Mvc.HtmlHelper htmlHelper, string formId)
        {
            if (MvcHtmlString.IsNullOrEmpty(htmlHelper.ValidationSummary()))
            {
                return InitValidatorsScript(formId);
            }
            var result = htmlHelper.ValidationSummary().ToHtmlString() + InitValidatorsScript(formId);
            return new MvcHtmlString(result);
        }


        /// <summary>
        /// 提示容器
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="tip"></param>
        /// <returns></returns>
        public static MvcHtmlString ValidationMessageExFor<TModel, TProperty>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TProperty>> expression,
           object htmlAttributes = null,
           string tip = "Tip")
        {
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            var tagerHtmlAtt = attrs.ToDictionary(o => o.Key, o => o.Value);

            var oupString = new StringBuilder();
            oupString.Append(string.Format("<div id=\"{0}Tip\" ", ExpressionHelper.GetExpressionText(expression)));
            foreach (var o in tagerHtmlAtt)
            {
                oupString.Append(string.Format(" {0}='{1}' ", o.Key, o.Value));
            }
            oupString.Append("></div>");
            return new MvcHtmlString(oupString.ToString());
        }

        /// <summary>
        /// 提示容器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="tip"></param>
        /// <returns></returns>
        public static MvcHtmlString ValidationMessageEx(
            this System.Web.Mvc.HtmlHelper htmlHelper,
            string name,
           object htmlAttributes = null,
           string tip = "Tip")
        {
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            var tagerHtmlAtt = attrs.ToDictionary(o => o.Key, o => o.Value);

            var oupString = new StringBuilder();
            oupString.Append(string.Format("<div id=\"{0}Tip\" ", name));
            foreach (var o in tagerHtmlAtt)
            {
                oupString.Append(string.Format(" {0}='{1}' ", o.Key, o.Value));
            }
            oupString.Append("></div>");
            return new MvcHtmlString(oupString.ToString());
        }



        /// <summary>
        /// 验证密码文本框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static MvcHtmlString PasswordEx(
            this System.Web.Mvc.HtmlHelper htmlHelper,
            string name,
           object htmlAttributes,
           PassValidateOption option)
        {

            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            var tagerHtmlAtt = attrs.ToDictionary(o => o.Key, o => o.Value);

            if (option != null)
            {

                // 默认描述
                if (!string.IsNullOrEmpty(option.Description))
                {
                    tagerHtmlAtt.Add("description", option.Description);
                }
                // 失去焦点描述
                if (!string.IsNullOrEmpty(option.FocusMsg))
                {
                    tagerHtmlAtt.Add("focusMsg", option.FocusMsg);
                }
                // 范围验证
                if (option.Min != null)
                {
                    tagerHtmlAtt.Add("min", option.Min);
                    if (!string.IsNullOrEmpty(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }

                // 非空验证
                if (option.IsEmpty)
                {
                    tagerHtmlAtt.Add("isEmpty", "true");
                }
                else
                {
                    if (!string.IsNullOrEmpty(option.EmptyMsg))
                    {
                        tagerHtmlAtt.Add("emptyMsg", option.EmptyMsg);
                    }
                    tagerHtmlAtt.Add("isEmpty", "false");
                }

                if (option.Max != null)
                {
                    tagerHtmlAtt.Add("max", option.Max);
                    if (option.RangeMsg != null && tagerHtmlAtt.ContainsKey(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }
                if (option.CompareElem != null)
                {
                    tagerHtmlAtt.Add("compareElemId", option.CompareElem);
                    if (!string.IsNullOrEmpty(option.CompareElemMsg))
                    {
                        tagerHtmlAtt.Add("compareElemMsg", option.CompareElemMsg);
                    }
                }
            }

            tagerHtmlAtt.Add("validator", "true");
            return htmlHelper.Password(name, tagerHtmlAtt);
        }



        /// <summary>
        /// 验证密码文本框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static MvcHtmlString PasswordExFor<TModel, TProperty>(
           this HtmlHelper<TModel> htmlHelper,
           Expression<Func<TModel, TProperty>> expression,
           object htmlAttributes,
           PassValidateOption option)
        {

            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            var tagerHtmlAtt = attrs.ToDictionary(o => o.Key, o => o.Value);

            if (option != null)
            {

                // 默认描述
                if (!string.IsNullOrEmpty(option.Description))
                {
                    tagerHtmlAtt.Add("description", option.Description);
                }
                // 失去焦点描述
                if (!string.IsNullOrEmpty(option.FocusMsg))
                {
                    tagerHtmlAtt.Add("focusMsg", option.FocusMsg);
                }
                // 范围验证
                if (option.Min != null)
                {
                    tagerHtmlAtt.Add("min", option.Min);
                    if (!string.IsNullOrEmpty(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }

                // 非空验证
                if (option.IsEmpty)
                {
                    tagerHtmlAtt.Add("isEmpty", "true");
                }
                else
                {
                    if (!string.IsNullOrEmpty(option.EmptyMsg))
                    {
                        tagerHtmlAtt.Add("emptyMsg", option.EmptyMsg);
                    }
                    tagerHtmlAtt.Add("isEmpty", "false");
                }

                if (option.Max != null)
                {
                    tagerHtmlAtt.Add("max", option.Max);
                    if (option.RangeMsg != null && tagerHtmlAtt.ContainsKey(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }
                if (option.CompareElem != null)
                {
                    tagerHtmlAtt.Add("compareElemId", option.CompareElem);
                    if (!string.IsNullOrEmpty(option.CompareElemMsg))
                    {
                        tagerHtmlAtt.Add("compareElemMsg", option.CompareElemMsg);
                    }
                }
            }

            tagerHtmlAtt.Add("validator", "true");
            return htmlHelper.PasswordFor(expression, tagerHtmlAtt);
        }

        /// <summary>
        /// 验证文本框
        /// </summary>
        /// <param name="name">验证文本框Name</param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option">验证规则</param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxEx(
            this HtmlHelper htmlHelper,
            string name,
            object htmlAttributes,
            ValidateOption option = null
          )
        {
            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            var tagerHtmlAtt = attrs.ToDictionary(o => o.Key, o => o.Value);

            if (option != null)
            {

                // 默认描述
                if (!string.IsNullOrEmpty(option.Description))
                {
                    tagerHtmlAtt.Add("description", option.Description);
                }
                // 失去焦点描述
                if (!string.IsNullOrEmpty(option.FocusMsg))
                {
                    tagerHtmlAtt.Add("focusMsg", option.FocusMsg);
                }
                // 范围验证
                if (option.Min != null)
                {
                    tagerHtmlAtt.Add("min", option.Min);
                    if (!string.IsNullOrEmpty(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }

                // 非空验证
                if (option.IsEmpty)
                {
                    tagerHtmlAtt.Add("isEmpty", "true");
                }
                else
                {
                    if (!string.IsNullOrEmpty(option.EmptyMsg))
                    {
                        tagerHtmlAtt.Add("emptyMsg", option.EmptyMsg);
                    }
                    tagerHtmlAtt.Add("isEmpty", "false");
                }


                //正则验证
                if (!string.IsNullOrEmpty(option.Regex))
                {
                    tagerHtmlAtt.Add("regex", option.Regex);
                    tagerHtmlAtt.Add("regexMsg", option.RegexMsg);
                }


                if (option.Max != null)
                {
                    tagerHtmlAtt.Add("max", option.Max);
                    if (option.RangeMsg != null && tagerHtmlAtt.ContainsKey(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }
                if (option.AjaxUrl != null)
                {
                    tagerHtmlAtt.Add("ajax", option.AjaxUrl);
                }
                if (option.CompareElem != null)
                {
                    tagerHtmlAtt.Add("compareElemId", option.CompareElem);
                    if (!string.IsNullOrEmpty(option.CompareElemMsg))
                    {
                        tagerHtmlAtt.Add("compareElemMsg", option.CompareElemMsg);
                    }
                }
            }

            tagerHtmlAtt.Add("validator", "true");
            return htmlHelper.TextBox(name, null, tagerHtmlAtt);
        }

        /// <summary>
        /// 验证文本域
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option">验证规则</param>
        /// <returns></returns>
        public static MvcHtmlString TextAreaExFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes,
            ValidateOption option = null
          )
        {
            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            var tagerHtmlAtt = attrs.ToDictionary(o => o.Key, o => o.Value);

            if (option != null)
            {

                // 默认描述
                if (!string.IsNullOrEmpty(option.Description))
                {
                    tagerHtmlAtt.Add("description", option.Description);
                }
                // 失去焦点描述
                if (!string.IsNullOrEmpty(option.FocusMsg))
                {
                    tagerHtmlAtt.Add("focusMsg", option.FocusMsg);
                }
                // 范围验证
                if (option.Min != null)
                {
                    tagerHtmlAtt.Add("min", option.Min);
                    if (!string.IsNullOrEmpty(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }


                // 非空验证
                if (option.IsEmpty)
                {
                    tagerHtmlAtt.Add("isEmpty", "true");
                }
                else
                {
                    if (!string.IsNullOrEmpty(option.EmptyMsg))
                    {
                        tagerHtmlAtt.Add("emptyMsg", option.EmptyMsg);
                    }
                    tagerHtmlAtt.Add("isEmpty", "false");
                }


                //正则验证
                if (!string.IsNullOrEmpty(option.Regex))
                {
                    tagerHtmlAtt.Add("regex", option.Regex);
                    tagerHtmlAtt.Add("regexMsg", option.RegexMsg);
                }

                if (option.Max != null)
                {
                    tagerHtmlAtt.Add("max", option.Max);
                    if (option.RangeMsg != null && tagerHtmlAtt.ContainsKey(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }
                if (option.AjaxUrl != null)
                {
                    tagerHtmlAtt.Add("ajax", option.AjaxUrl);
                }
                if (option.CompareElem != null)
                {
                    tagerHtmlAtt.Add("compareElemId", option.CompareElem);
                    if (!string.IsNullOrEmpty(option.CompareElemMsg))
                    {
                        tagerHtmlAtt.Add("compareElemMsg", option.CompareElemMsg);
                    }
                }
            }

            tagerHtmlAtt.Add("validator", "true");
            return htmlHelper.TextAreaFor(expression, tagerHtmlAtt);
        }

        /// <summary>
        /// 验证文本框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option">验证规则</param>
        /// <returns></returns>
        public static MvcHtmlString TextBoxExFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes,
            ValidateOption option = null
          )
        {
            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            var tagerHtmlAtt = attrs.ToDictionary(o => o.Key, o => o.Value);

            if (option != null)
            {

                // 默认描述
                if (!string.IsNullOrEmpty(option.Description))
                {
                    tagerHtmlAtt.Add("description", option.Description);
                }
                // 失去焦点描述
                if (!string.IsNullOrEmpty(option.FocusMsg))
                {
                    tagerHtmlAtt.Add("focusMsg", option.FocusMsg);
                }
                // 范围验证
                if (option.Min != null)
                {
                    tagerHtmlAtt.Add("min", option.Min);
                    if (!string.IsNullOrEmpty(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }

                // 非空验证
                if (option.IsEmpty)
                {
                    tagerHtmlAtt.Add("isEmpty", "true");
                }
                else
                {
                    if (!string.IsNullOrEmpty(option.EmptyMsg))
                    {
                        tagerHtmlAtt.Add("emptyMsg", option.EmptyMsg);
                    }
                    tagerHtmlAtt.Add("isEmpty", "false");
                }


                //正则验证
                if (!string.IsNullOrEmpty(option.Regex))
                {
                    tagerHtmlAtt.Add("regex", option.Regex);
                    tagerHtmlAtt.Add("regexMsg", option.RegexMsg);
                }

                if (option.Max != null)
                {
                    tagerHtmlAtt.Add("max", option.Max);
                    if (option.RangeMsg != null && tagerHtmlAtt.ContainsKey(option.RangeMsg))
                    {
                        tagerHtmlAtt.Add("rangeMsg", option.RangeMsg);
                    }
                }

                if (option.AjaxUrl != null)
                {
                    tagerHtmlAtt.Add("ajax", option.AjaxUrl);
                }

                if (option.CompareElem != null)
                {
                    tagerHtmlAtt.Add("compareElemId", option.CompareElem);
                    if (!string.IsNullOrEmpty(option.CompareElemMsg))
                    {
                        tagerHtmlAtt.Add("compareElemMsg", option.CompareElemMsg);
                    }
                }

            }
            else
            {
                tagerHtmlAtt.Add("isEmpty", "true");
            }

            tagerHtmlAtt.Add("validator", "true");
            return htmlHelper.TextBoxFor(expression, tagerHtmlAtt);
        }


        /// <summary>
        /// 验证下拉框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option">验证规则</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListExFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes,
            ValidateOption option = null
            )
        {
            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            var tagerHtmlAtt = attrs.ToDictionary(o => o.Key, o => o.Value);
            if (option != null)
            {
                // 默认描述
                if (!string.IsNullOrEmpty(option.Description))
                {
                    tagerHtmlAtt.Add("description", option.Description);
                }
                // 失去焦点描述
                if (!string.IsNullOrEmpty(option.FocusMsg))
                {
                    tagerHtmlAtt.Add("focusMsg", option.FocusMsg);
                }

                // 非空验证
                if (option.IsEmpty)
                {
                    tagerHtmlAtt.Add("isEmpty", "true");
                }
                else
                {
                    if (!string.IsNullOrEmpty(option.EmptyMsg))
                    {
                        tagerHtmlAtt.Add("emptyMsg", option.EmptyMsg);
                    }
                    tagerHtmlAtt.Add("isEmpty", "false");
                }
                if (option.AjaxUrl != null)
                {
                    tagerHtmlAtt.Add("ajax", option.AjaxUrl);
                }
                if (option.AjaxFun != null)
                {
                    tagerHtmlAtt.Add("ajaxFun", option.AjaxFun);
                }
            }
            else
            {
                tagerHtmlAtt.Add("isEmpty", "true");
            }
            tagerHtmlAtt.Add("validator", "true");
            return htmlHelper.DropDownListFor(expression, new List<SelectListItem>());
            //return htmlHelper.DropDownList(name, null /* selectList */, null /* optionLabel */, tagerHtmlAtt);
        }


        /// <summary>
        /// 验证下拉框
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListEx(
            this HtmlHelper htmlHelper,
            string name,
            object htmlAttributes = null,
            ValidateOption option = null
            )
        {
            // 拼装验证属性 
            var attrs = ((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes));
            var tagerHtmlAtt = attrs.ToDictionary(o => o.Key, o => o.Value);
            if (option != null)
            {
                // 默认描述
                if (!string.IsNullOrEmpty(option.Description))
                {
                    tagerHtmlAtt.Add("description", option.Description);
                }
                // 失去焦点描述
                if (!string.IsNullOrEmpty(option.FocusMsg))
                {
                    tagerHtmlAtt.Add("focusMsg", option.FocusMsg);
                }

                // 非空验证
                if (option.IsEmpty)
                {
                    tagerHtmlAtt.Add("isEmpty", "true");
                }
                else
                {
                    if (!string.IsNullOrEmpty(option.EmptyMsg))
                    {
                        tagerHtmlAtt.Add("emptyMsg", option.EmptyMsg);
                    }
                    tagerHtmlAtt.Add("isEmpty", "false");
                }
                if (option.AjaxUrl != null)
                {
                    tagerHtmlAtt.Add("ajax", option.AjaxUrl);
                }
                if (option.AjaxFun != null)
                {
                    tagerHtmlAtt.Add("ajaxFun", option.AjaxFun);
                }
            }
            else
            {
                tagerHtmlAtt.Add("isEmpty", "true");
            }
            tagerHtmlAtt.Add("validator", "true");

            return htmlHelper.DropDownList(name, null /* selectList */, null /* optionLabel */, tagerHtmlAtt);
        }




        /// <summary>  
        /// 分页Pager显示  
        /// </summary>   
        /// <param name="html"></param>  
        /// <param name="currentPageStr">标识当前页码的QueryStringKey</param>    
        /// <param name="pageSize">每页显示</param>  
        /// <param name="totalCount">总数据量</param>
        /// <param name="requestUrl">请求URL</param>
        /// <returns></returns> 
        public static MvcHtmlString Pager(
            this HtmlHelper html,
            string currentPageStr,
            int pageSize,
            int totalCount,
            string requestUrl = null)
        {

            // 当前请求
            var request = HttpContext.Current.Request;

            var requestQueryStr = request.QueryString;
            var parameter = requestQueryStr.AllKeys.
                Where(str => !string.IsNullOrEmpty(str) && str.ToLower() != currentPageStr.ToLower()).
                Aggregate("", (current, str) => current + string.Format("&{0}={1}", str, requestQueryStr[str]));

            // Url地址
            var url = request.CurrentExecutionFilePath;
            // 当前页码
            var currentPage = 1;
            //总页数
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1);
            // 其他附带请求参数
            int.TryParse(request[currentPageStr], out currentPage);
            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            var output = new StringBuilder("<div pagerId='pageNav' class='pageNav'>");
            if (totalPages > 1)
            {
                //处理首页连接    
                output.AppendFormat("<a href='{0}?{1}={2}{3}'>{4}</a>", url, currentPageStr, 1, parameter, "首页");
                output.Append(" ");
                int currint = 5;
                for (int i = 0; i <= 10; i++)
                {
                    //一共最多显示10位页码，前面5位，后面5位  
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                        if (currint == i)
                        {
                            //当前页处理  
                            output.Append(string.Format("<strong>{0}</strong>", currentPage));
                        }
                        else
                        {
                            //一般页处理  
                            output.AppendFormat("<a href='{0}?{1}={2}{3}'>{4}</a>",
                                url,
                                currentPageStr,
                                (currentPage + i - currint),
                                parameter,
                                (currentPage + i - currint));

                        }

                }
                output.Append(" ");
                output.AppendFormat("<a href='{0}?{1}={2}{3}'>{4}</a>",
                               url,
                               currentPageStr,
                               totalPages,
                               parameter,
                              "末页");
                output.Append(" ");
            }
            output.Append("</div>");
            return new MvcHtmlString(output.ToString());
        }



        #region Helper
        /// <summary>
        /// 生成客户端验证脚本
        /// </summary> 
        /// <returns></returns>
        private static MvcHtmlString InitValidatorsScript(string formId)
        {
            var outPut = new StringBuilder();
            outPut.Append(@"<script type='text/javascript'>");
            outPut.Append(@"$(function () {");
            outPut.Append(string.Format(@"$('#{0}').formValidator();", formId));
            outPut.Append(@"});");
            outPut.Append(@"</script>");
            return new MvcHtmlString(outPut.ToString());
        }
        #endregion

    }
}