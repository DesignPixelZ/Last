using System.IO;

namespace Last.Framework.Content.Data.Mesh
{
    public class MeshCollisionCell
    {
        public ushort A { get; private set; }
        public ushort B { get; private set; }
        public ushort C { get; private set; }
        public ushort unkUShort0 { get; private set; } //0 in all samples (support quads?)

        //TODO: Verify
        public MeshCollisionFlag unkByte0 { get; private set; }

        public MeshCollisionCell(BinaryReader reader, uint unkUInt1)
        {
            this.A = reader.ReadUInt16();
            this.B = reader.ReadUInt16();
            this.C = reader.ReadUInt16();
            this.unkUShort0 = reader.ReadUInt16();

            if (unkUInt1 == 6 || unkUInt1 == 7 || unkUInt1 == 14)
                this.unkByte0 = (MeshCollisionFlag)reader.ReadByte();
        }
    }
}