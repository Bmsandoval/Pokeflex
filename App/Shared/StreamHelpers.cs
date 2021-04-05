using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace App.Shared
{
    public class StreamHelpers {
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