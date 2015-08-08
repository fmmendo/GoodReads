using MyShelf.API.XML;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MyShelf.API.Services
{
    public class ServiceBase
    {
        /// <summary>
        /// Deserializes a GoodReads response XML
        /// </summary>
        /// <param name="xml">xml data</param>
        /// <returns>GoodreadsResponse object</returns>
        protected static GoodreadsResponse DeserializeResponse(string xml)
        {
            GoodreadsResponse response = null;

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(GoodreadsResponse));

                    response = (GoodreadsResponse)serializer.Deserialize(stream);
                }
                catch (Exception)
                {
                }
            }
            return response;
        }

        /// <summary>
        /// Deserializes XML
        /// </summary>
        /// <param name="xml">xml data</param>
        /// <returns>deserialized object</returns>
        private static T DeserializeResponse<T>(string xml)
        {
            T response = default(T);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(T));

                    response = (T)serializer.Deserialize(stream);
                }
                catch (Exception)
                {
                }
            }
            return response;
        }
    }
}
