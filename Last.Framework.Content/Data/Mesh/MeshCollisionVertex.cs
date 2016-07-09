using SharpDX;
using System.IO;

namespace Last.Framework.Content.Data.Mesh
{
    public class MeshCollisionVertex
    {
        public Vector3 Position { get; private set; }
        public byte unkByte0 { get; private set; }

        public MeshCollisionVertex(BinaryReader reader)
        {
            this.Position = reader.ReadVector3();
            this.unkByte0 = reader.ReadByte();
        }
    }
}