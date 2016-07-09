using System.IO;

namespace Replace.Framework.Common.Security
{
    internal class PacketWriter : BinaryWriter
    {
        public PacketWriter() : base(new MemoryStream())
        {
        }

        public byte[] GetBytes()
        {
            return ((MemoryStream)base.OutStream).ToArray();
        }
    }
}