using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;
using Newtonsoft.Json.Converters; 
using Peacock.DataBase.Service.App_Start;
using Peacock.DataBase.Service.App_Start.Formatter; 

namespace Peacock.DataBase.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

             config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });   

            config.Routes.Cast<HttpRoute>().ToArray();

            // 删除Json/Xml格式化器规则处理
            //config.Formatters.Remove(config.Formatters.JsonFormatter); 
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            // 注入自定义Json/Xml格式化器规则处理
            config.Formatters.Insert(0,new JsonNetFormatter()); 
            config.Formatters.Insert(1,new XmlNetFormatter()); 
            // 根据参数选择 格式化处理
            GlobalConfiguration.Configuration.Formatters[0].AddQueryStringMapping("type", "json", "application/json");
            GlobalConfiguration.Configuration.Formatters[1].AddQueryStringMapping("type", "xml", "application/xml");

        }
          

    }
}
