using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Omikron.SharedKernel.Extensions
{
    public static class ByteSerializerExtensions
    {
        public static byte[] Serialize<TObject>(this TObject m)
        {
            using (var ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, m);
                return ms.ToArray();
            }
        }

        public static TObject Deserialize<TObject>(this byte[] byteArray)
        {
            if (byteArray != null && byteArray.Length > 0)
            {
                using (var ms = new MemoryStream(byteArray))
                {
                    return (TObject)new BinaryFormatter().Deserialize(ms);
                }
            }

            return default(TObject);
        }
    }
}
