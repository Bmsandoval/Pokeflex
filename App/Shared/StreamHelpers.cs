using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace App.Shared
{
    public class StreamHelpers {
        public static MemoryStream ToJsonStream<T>(T obj)
        {
            return StreamHelpers.SerializeToStream(ToJsonString(obj));
        }

        public static string ToJsonString<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new BinaryConverter());
        }

        public static T FromJsonStream<T>(MemoryStream stream)
        {
            return FromJsonString<T>(StreamHelpers.DeserializeFromStream(stream));
        }

        public static T FromJsonString<T>(string json)
        {
            return JToken.Parse(json).ToObject<T>();
        }
        
        public static MemoryStream SerializeToStream(string data)
        {
            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            writer.WriteLine(data);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string DeserializeFromStream(MemoryStream stream)
        {
            StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
    } 
}