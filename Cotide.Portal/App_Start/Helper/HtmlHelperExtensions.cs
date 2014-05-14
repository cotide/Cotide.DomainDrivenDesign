using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.Routing; 
using System.Configuration;
using Cotide.Framework.Extensions;

namespace System.Web.Mvc
{
    /// <summary>
    /// System.Web.Mvc.ViewMasterPage 扩展HTML工具类
    /// </summary>
    public static class HtmlHelperExtensions
    {
        private const string BaseRootPath = "~/";

        /// <summary>
        /// 版本
        /// </summary>
        private static string _version = ConfigurationManager.AppSettings["Version"];

        /// <summary>
        /// 脚本根文件夹位置
        /// </summary>
        private const string ScriptRootPath =BaseRootPath+ "scripts/";

        /// <summary>
        /// 样式根文件夹位置
        /// </summary>
        private const string CssRootPath = BaseRootPath + "content/";

        /// <summary>
        /// 生成CSS的链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MvcHtmlString StyleSheet(this HtmlHelper html, string fileName)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var tagBuilder = new TagBuilder("link");

            tagBuilder.MergeAttribute("href", urlHelper.Content(CssRootPath+fileName) .GetVersionPar(_version));
            tagBuilder.MergeAttribute("rel", "stylesheet");
            tagBuilder.MergeAttribute("type", "text/css");

            return new MvcHtmlString(tagBuilder.ToString()); 
        }
         
        /// <summary>
        /// 生成Scrip的链接
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MvcHtmlString JavaScript(
            this HtmlHelper html, 
            string fileName )
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var tagBuilder = new TagBuilder("script");

            tagBuilder.MergeAttribute("src", urlHelper.Content(ScriptRootPath + fileName).GetVersionPar(_version));
            tagBuilder.MergeAttribute("type", "text/javascript");
            return new MvcHtmlString(tagBuilder.ToString()); 
        }

        /// <summary>
        /// 生成Jquery脚本连接
        /// </summary>
        /// <param name="html"></param> 
        /// <returns></returns>
        public static MvcHtmlString JqueryScript(this HtmlHelper html)
        {
            var jqueryUrl = ConfigurationManager.AppSettings["JQueryLib"];
            if (string.IsNullOrEmpty(jqueryUrl))
                return new MvcHtmlString(""); 
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var tagBuilder = new TagBuilder("script");
            tagBuilder.MergeAttribute("src", urlHelper.Content(jqueryUrl).GetVersionPar(_version));
            tagBuilder.MergeAttribute("type", "text/javascript"); 
            return new MvcHtmlString(tagBuilder.ToString());
        }

        /// <summary>
        /// 生成Signalr脚本
        /// </summary>
        /// <param name="htm"></param>
        /// <returns></returns>
        public static MvcHtmlString SignalrScript(this HtmlHelper html)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var tagBuilder = new TagBuilder("script");
            tagBuilder.MergeAttribute("src", urlHelper.Content("~/Signalr/Hubs").GetVersionPar(_version));
            tagBuilder.MergeAttribute("type", "text/javascript");
            return new MvcHtmlString(tagBuilder.ToString()); 
        }

        /// <summary>
        /// 获取URL地址
        /// </summary>
        /// <param name="html"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static MvcHtmlString Url(this HtmlHelper html,string fileName="")
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            return new MvcHtmlString(urlHelper.Content(BaseRootPath + fileName).GetVersionPar(_version)); 
        }
         
    }  
}