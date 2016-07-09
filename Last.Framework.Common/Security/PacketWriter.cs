using System.IO;

namespace Last.Framework.Common.Security
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