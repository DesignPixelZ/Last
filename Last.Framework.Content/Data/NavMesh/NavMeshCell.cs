using SharpDX;
using System.Collections.Generic;
using System.IO;

namespace Last.Framework.Content.Data.NavMesh
{
    public class NavMeshCell
    {
        public RectangleF Rectangle { get; set; }

        private List<ushort> _entryIndices;

        public IReadOnlyList<ushort> Entries => _entryIndices;

        public NavMeshCell(BinaryReader reader)
        {
            this.Read(reader);
        }

        public ushort this[ushort index]
        {
            get { return _entryIndices[index]; }
            set { _entryIndices[index] = value; }
        }

        public void Read(BinaryReader reader)
        {
            this.Rectangle = reader.ReadRectangleF();

            byte entryCount = reader.ReadByte();
            _entryIndices = new List<ushort>(entryCount);
            for (int i = 0; i < entryCount; i++)
            {
                _entryIndices.Add(reader.ReadUInt16()); //EntryIndex
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.Rectangle);

            writer.Write((byte)_entryIndices.Count);
            for (int i = 0; i < _entryIndices.Count; i++)
            {
                writer.Write(_entryIndices[i]);
            }
        }
    }
}