using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Peacock.DataBase.Helper.Utility;

namespace Peacock.DataBase.Service.App_Start.Formatter
{
    /// <summary>
    /// Xml 格式化规则
    /// </summary>
    public class XmlNetFormatter : MediaTypeFormatter
    {
        public XmlNetFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
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
                var xml = SerializerHelper.ObjectToXml(value); 
                var buffer = Encoding.UTF8.GetBytes(xml);
                writeStream.Write(buffer, 0, buffer.Length);
                writeStream.Flush();
            });
        }
    }
}