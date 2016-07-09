using Last.Framework.Utility;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Last.Framework.Content.Data.Mesh
{
    public class MeshFile : ContentFile
    {
        private const string SUPPORTED_HEADER = "JMXVBMS 0110";

        private MeshOffset _offsets;
        private MeshFlags _flags;

        public string Name { get; set; }
        public string Material { get; set; }
        public uint unkUInt4 { get; set; }

        public BoundingBox AxisAlignedBoundingBox;

        public List<MeshCollisionVertex> CollisionVertices;
        public List<MeshCollisionCell> CollisionCells; //ObjectGround
        public List<MeshCollisionLink> CollisionOuterLinks; //ObjectOutlines
        public List<MeshCollisionLink> CollisionInnerLinks; //ObjectInlines

        public MeshFile(ContentManager contentManager, FileInfo file) : base(contentManager, file)
        {
        }

        public override void Load(Stream stream, ContentPurpose purpose)
        {
            var funcName = $"{nameof(MeshFile)}->{Caller.GetMemberName()}";

            using (var reader = new BinaryReader(stream))
            {
                _header = reader.ReadChars(SUPPORTED_HEADER.Length);
                if (this.Header != SUPPORTED_HEADER)
                {
                    Console.WriteLine($"{funcName}: Unsupported header detected! (Value = {this.Header}) [File:{this.File.Name}]");
                    return;
                }

                var offsetBuffer = reader.ReadBytes(Marshal.SizeOf(_offsets));
                _offsets = Unmanaged.BufferToStruct<MeshOffset>(offsetBuffer);

                if (_offsets.Unknown8 != 0)
                    Console.WriteLine($"{funcName}: {nameof(_offsets.Unknown8)} != 0 (Value = {_offsets.Unknown8}) [File:{this.File.Name}]");

                var flagBuffer = reader.ReadBytes(Marshal.SizeOf(_flags));
                _flags = Unmanaged.BufferToStruct<MeshFlags>(flagBuffer);

#if DEBUG_CONTENT
                //if (unkUInt0 != 0)
                //    Console.WriteLine($"{funcName}: {nameof(unkUInt0)} != 0 (Value = {unkUInt0}) [File:{this.File.Name}]");
                if (_flags.unkUInt1 != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{funcName}: {nameof(_flags.unkUInt1)} != 0 (Value = {_flags.unkUInt1}) [File:{this.File.Name}]");
                    Console.ResetColor();
                }

                //if (unkUInt2 != 0)
                //Console.WriteLine($"{funcName}: {nameof(unkUInt2)} != 0 (Value = {unkUInt2}) [File:{this.File.Name}]");
                //if (unkUInt3 != 0)
                //Console.WriteLine($"{funcName}: {nameof(unkUInt3)} != 0 (Value = {unkUInt3}) [File:{this.File.Name}]");
#endif

                Name = reader.ReadKoreanString();
                Material = reader.ReadKoreanString();
                unkUInt4 = reader.ReadUInt32();

#if DEBUG_CONTENT
                //if (unkUInt4 != 0)
                //    Console.WriteLine($"{funcName}: {nameof(unkUInt4)} != 0 (Value = {unkUInt4}) [File:{this.File.Name}]");
#endif

                if (purpose.HasFlags(ContentPurpose.Visual))
                {
                    throw new NotImplementedException();

                    ////Vertices
                    //this.ValidatePointer(reader, MeshOffsetType.Verticies);
                    //this.loadVertices(reader);

                    ////Bones
                    //this.ValidatePointer(reader, MeshOffsetType.Bones);
                    //this.loadBones(reader);

                    ////Faces
                    //this.ValidatePointer(reader, MeshOffsetType.Indices);
                    //this.loadIndices(reader);

                    ////Unkown3
                    //this.ValidatePointer(reader, MeshOffsetType.Unknown3);
                    //this.loadUnknown3(reader);

                    ////Unknown4
                    //this.ValidatePointer(reader, MeshOffsetType.Unknown4);
                    //this.loadUnknown4(reader);
                }

                //BoundingBox
                this.ValidatePointer(reader, MeshOffsetType.BoundingBox);
                AxisAlignedBoundingBox = reader.ReadBoundingBox();

                //Gates
                //this.ValidatePointer(reader, MeshOffsetType.Gates);
                //this.loadGates(reader);

                //this.ValidatePointer(reader, MeshOffsetType.Unknown9);
                //this.loadUnknown9(reader);

                //Collision
                if (_offsets.Collision != 0 && purpose.HasFlags(ContentPurpose.Collision))
                {
                    this.ValidatePointer(reader, MeshOffsetType.Collision);
                    this.loadCollision(reader);
                }

#if DEBUG_EOF
                //EOF
                if (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{funcName}: EOF mismatch! ({reader.BaseStream.Length - reader.BaseStream.Position} bytes remaining) [File:{this.File.Name}]");
                    Console.ResetColor();
                }
#endif
            }
        }

        private void loadCollision(BinaryReader reader)
        {
            var vertexCount = reader.ReadInt32();
            this.CollisionVertices = new List<MeshCollisionVertex>(vertexCount);
            for (int i = 0; i < vertexCount; i++)
            {
                var vertex = new MeshCollisionVertex(reader);
                CollisionVertices.Add(vertex);
            }

            var cellCount = reader.ReadInt32();
            this.CollisionCells = new List<MeshCollisionCell>(cellCount);
            for (int i = 0; i < cellCount; i++)
            {
                var cell = new MeshCollisionCell(reader, _flags.unkUInt1);
                CollisionCells.Add(cell);
            }

            var outlineLinkCount = reader.ReadInt32();
            this.CollisionOuterLinks = new List<MeshCollisionLink>(outlineLinkCount);
            for (int i = 0; i < outlineLinkCount; i++)
            {
                var link = new MeshCollisionLink(reader, _flags.unkUInt1);
                CollisionOuterLinks.Add(link);
            }

            var inlineLinkCount = reader.ReadInt32();
            this.CollisionInnerLinks = new List<MeshCollisionLink>(inlineLinkCount);
            for (int i = 0; i < inlineLinkCount; i++)
            {
                var link = new MeshCollisionLink(reader, _flags.unkUInt1);
                CollisionInnerLinks.Add(link);
            }

            if (_flags.unkUInt1 == 4 ||
                _flags.unkUInt1 == 5 ||
                _flags.unkUInt1 == 6 ||
                _flags.unkUInt1 == 7 ||
                _flags.unkUInt1 == 14)
            {
                var eventCount = reader.ReadUInt32();
                for (int i = 0; i < eventCount; i++)
                {
                    string eventName = reader.ReadKoreanString();
                    //Console.WriteLine("Event: " + eventName);
                }
            }

            var float0 = reader.ReadSingle();
            var float1 = reader.ReadSingle();
            var uint0 = reader.ReadUInt32();
            var uint1 = reader.ReadUInt32();

            var count = reader.ReadUInt32();
            for (int u = 0; u < count; u++)
            {
                uint count2 = reader.ReadUInt32();
                for (int ii = 0; ii < count2; ii++)
                {
                    reader.ReadUInt16();
                }
            }
        }

        private void ValidatePointer(BinaryReader reader, MeshOffsetType offsetType)
        {
#if DEBUG_POINTER
            if (reader.BaseStream.Position != _offsets[offsetType])
            {
                var funcName = $"{nameof(MeshFile)}->{Caller.GetMemberName()}";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{funcName}: {offsetType} mismatch! ({reader.BaseStream.Position - _offsets[offsetType]} bytes) [File:{this.File.Name}]");
                Console.ResetColor();
            }
#endif
            reader.BaseStream.Position = _offsets[offsetType];
        }

        public override void Save(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}