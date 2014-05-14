using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using Peacock.DataBase.Helper.Utility;

namespace Peacock.DataBase.Service.App_Start
{
    /// <summary>
    /// Json 格式化规则
    /// </summary>
    public class JsonNetFormatter : MediaTypeFormatter
    {
        public JsonNetFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override Task WriteToStreamAsync(
            Type type,
            object value,
            System.IO.Stream writeStream,
            System.Net.Http.HttpContent content,
            System.Net.TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Auto,
                    TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                    Formatting = Formatting.Indented ,
                    DateFormatString = "yyyy-MM-dd hh:mm:ss"
                };
                
                JsonSerializer serializer = JsonSerializer.Create(settings);
                using (var streamWriter = new StreamWriter(writeStream, Encoding.UTF8))
                {
                    using (var jsonTextWriter = new JsonTextWriter(streamWriter))
                    {
                        serializer.Serialize(jsonTextWriter, value);
                    }
                }  
            });
        }
    }



}