using SharpDX;
using System.Collections.Generic;
using System.IO;

namespace Last.Framework.Content.Data.NavMesh
{
    public class NavMeshEntry
    {
        //TODO: Analyze Extra and unkUShorts...

        public uint ID { get; set; }
        public Vector3 Position { get; set; }
        public NavMeshEntryCollision Collision { get; set; }
        public AngleSingle Angle { get; set; }
        public ushort UniqueID { get; set; }

        internal ushort unkUShort0;
        internal ushort unkUShort1;

        public ushort RegionID { get; set; }

        internal List<byte[]> Extra;

        internal NavMeshEntry(BinaryReader reader)
        {
            this.Read(reader);
        }

        internal void Read(BinaryReader reader)
        {
            this.ID = reader.ReadUInt32();
            this.Position = reader.ReadVector3();
            this.Collision = (NavMeshEntryCollision)reader.ReadUInt16();
            this.Angle = new AngleSingle(reader.ReadSingle(), AngleType.Radian);
            this.UniqueID = reader.ReadUInt16();

            unkUShort0 = reader.ReadUInt16();
            unkUShort1 = reader.ReadUInt16(); //passable or some shit?

#if DEBUG_CONTENT
            var funcName = $"{nameof(NavMeshEntry)}->{Caller.GetMemberName()}";
            if (Unk4 != 0)
                Console.WriteLine($"{funcName}: {nameof(Unk4)} != 0 (Value = {Unk4}) [Entry:{ID}, UID:{UniqueID}]");
            if (Unk5 != 0)
                Console.WriteLine($"{funcName}: {nameof(Unk5)} != 0 (Value = {Unk5}) [Entry:{ID}, UID:{UniqueID}]");
#endif

            this.RegionID = reader.ReadUInt16();

            this.Extra = new List<byte[]>(reader.ReadUInt16());
            for (int i = 0; i < Extra.Capacity; i++)
            {
                Extra.Add(reader.ReadBytes(6));
            }
        }

        internal void Write(BinaryWriter writer)
        {
            writer.Write(this.ID);
            writer.Write(this.Position);
            writer.Write((ushort)this.Collision);
            writer.Write(this.Angle.Radians);
            writer.Write(this.UniqueID);

            writer.Write(this.unkUShort0);
            writer.Write(this.unkUShort1);

            writer.Write(this.RegionID);

            writer.Write((ushort)this.Extra.Count);
            for (int i = 0; i < this.Extra.Count; i++)
            {
                writer.Write(this.Extra[i]);
            }
        }
    }
}