using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace App.Shared
{
    public class StreamHelpers {
        public static MemoryStream ToJsonStream<T>(T obj)
        {
            return SerializeToStream(ToJsonString(obj));
        }

        public static string ToJsonString<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new BinaryConverter());
        }

        public static async Task<T> FromJsonStream<T>(MemoryStream stream)
        {
            return FromJsonString<T>(await DeserializeFromStream(stream));
        }

        public static T FromJsonString<T>(string json)
        {
            return JToken.Parse(json).ToObject<T>();
        }
        
        private static MemoryStream SerializeToStream(string data)
        {
            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            writer.WriteLine(data);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private static async Task<string> DeserializeFromStream(MemoryStream stream)
        {
            StreamReader reader = new(stream);
            return await reader.ReadToEndAsync();
        }

        public static async Task<T> DecodeBody<T>(Stream stream)
        {
            if (stream != null)
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true))
                {
                    return FromJsonString<T>(await reader.ReadToEndAsync());
                }
            }

            return default;
        }
    } 
}