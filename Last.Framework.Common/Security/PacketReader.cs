﻿using System.IO;

namespace Replace.Framework.Common.Security
{
    internal class PacketReader : BinaryReader
    {
        private byte[] _input;

        public PacketReader(byte[] input)
            : base(new MemoryStream(input, false))
        {
            _input = input;
        }

        public PacketReader(byte[] input, int index, int count)
            : base(new MemoryStream(input, index, count, false))
        {
            _input = input;
        }
    }
}