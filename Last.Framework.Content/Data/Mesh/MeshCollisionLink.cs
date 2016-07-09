using System.IO;

namespace Last.Framework.Content.Data.Mesh
{
    public class MeshCollisionLink
    {
        public ushort VertexSource { get; set; }
        public ushort VertexDestination { get; set; }
        public ushort CellSource { get; set; }
        public ushort CellDestination { get; set; }
        public MeshCollisionFlag Flag { get; set; }
        public MeshCollisionFlag unkByte0 { get; set; } //TODO: Verify

        public MeshCollisionLink(BinaryReader reader, uint unkUInt1)
        {
            this.VertexSource = reader.ReadUInt16();
            this.VertexDestination = reader.ReadUInt16();
            this.CellSource = reader.ReadUInt16();
            this.CellDestination = reader.ReadUInt16();
            this.Flag = (MeshCollisionFlag)reader.ReadByte();

            if (unkUInt1 == 5 || unkUInt1 == 7)
                this.unkByte0 = (MeshCollisionFlag)reader.ReadByte();
        }
    }
}