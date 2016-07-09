using SharpDX;
using System.IO;

namespace Last.Framework.Content.Data.NavMesh
{
    public class NavMeshRegionLink
    {
        //TODO: Marshalling

        public Vector2 Min { get; set; }
        public Vector2 Max { get; set; }

        public NavMeshLineFlag LineFlag { get; set; }

        public NavMeshLineDirection LineSource { get; set; }

        public NavMeshLineDirection LineDestination { get; set; }

        public ushort CellSource { get; set; }
        public ushort CellDestination { get; set; }

        public ushort RegionSource { get; set; }
        public ushort RegionDestination { get; set; }

        public NavMeshRegionLink(BinaryReader reader)
        {
            this.Read(reader);
        }

        public void Read(BinaryReader reader)
        {
            this.Min = reader.ReadVector2();
            this.Max = reader.ReadVector2();

            this.LineFlag = (NavMeshLineFlag)reader.ReadByte();
            this.LineSource = (NavMeshLineDirection)reader.ReadByte();
            this.LineDestination = (NavMeshLineDirection)reader.ReadByte();

            this.CellSource = reader.ReadUInt16();
            this.CellDestination = reader.ReadUInt16();

            this.RegionSource = reader.ReadUInt16();
            this.RegionDestination = reader.ReadUInt16();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.Min);
            writer.Write(this.Max);

            writer.Write((byte)this.LineFlag);

            writer.Write((byte)this.LineSource);
            writer.Write((byte)this.LineDestination);

            writer.Write(this.CellSource);
            writer.Write(this.CellDestination);

            writer.Write(this.CellSource);
            writer.Write(this.CellDestination);
        }
    }
}