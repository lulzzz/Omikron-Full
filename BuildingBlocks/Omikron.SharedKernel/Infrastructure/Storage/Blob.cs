using System.IO;

namespace Omikron.SharedKernel.Infrastructure.Storage
{
    public class Blob
    {
        public string Name { get; set; }
        public Stream Stream { get; set; }

        public Blob(string name, Stream stream)
        {
            Name = name;
            Stream = stream;
        }
    }
}