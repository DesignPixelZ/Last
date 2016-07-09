using Last.Framework.Content.Data.Mesh;
using Last.Framework.Utility;
using SharpDX;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Last.Framework.Content.Data.Resource
{
    public class ResourceFile : ContentFile
    {
        private const string SUPPORTED_HEADER = "JMXVRES 0109";

        private ResourceOffsets _offsets;
        private ResourceFlags _flags;

        public ResourceType Type { get; private set; }

        private byte[] unkBuffer;

        public string Name;

        public MeshFile CollisionMesh;
        public BoundingBox BoundingBoxOuter;
        public BoundingBox BoundingBoxInner;

        public ResourceFile(ContentManager manager, FileInfo file) : base(manager, file)
        {
        }

        public override void Load(Stream stream, ContentPurpose purpose)
        {
            var funcName = $"{nameof(ResourceFile)}->{Caller.GetMemberName()}";

            using (var reader = new BinaryReader(stream))
            {
                _header = reader.ReadChars(SUPPORTED_HEADER.Length);
                if (this.Header != SUPPORTED_HEADER)
                {
                    Console.WriteLine($"{funcName}: Unsupported header detected! (Value = {this.Header}) [File:{this.File.Name}]");
                    return;
                }

                var offsetBuffer = reader.ReadBytes(Marshal.SizeOf(_offsets));
                _offsets = Unmanaged.BufferToStruct<ResourceOffsets>(offsetBuffer);

                var flagBuffer = reader.ReadBytes(Marshal.SizeOf(_flags));
                _flags = Unmanaged.BufferToStruct<ResourceFlags>(flagBuffer);

                Type = (ResourceType)reader.ReadUInt32();
                Name = reader.ReadKoreanString();

                unkBuffer = reader.ReadBytes(48);

                //Collision
                if (purpose.HasFlags(ContentPurpose.Collision))
                {
                    this.ValidatePointer(reader, ResourceOffsetType.Collision);
                    this.loadCollision(reader);
                }

                if (purpose.HasFlags(ContentPurpose.Visual))
                {
                    throw new NotImplementedException();

                    ////Materials
                    //this.ValidatePointer(reader, ResourceOffsetType.Material);
                    //this.loadMaterialFiles(reader);

                    ////Meshes
                    //this.ValidatePointer(reader, ResourceOffsetType.Mesh);
                    //loadMeshFiles(reader);

                    ////Animations
                    //this.ValidatePointer(reader, ResourceOffsetType.Animation);
                    //this.loadAnimationFiles(reader);

                    ////Skeletons
                    //this.ValidatePointer(reader, ResourceOffsetType.Skeleton);
                    //this.loadSkeletonFiles(reader);

                    ////MeshGroups
                    //this.ValidatePointer(reader, ResourceOffsetType.MeshGroup);
                    //this.loadMeshGroups(reader);

                    ////Unknown 5
                    //this.ValidatePointer(reader, ResourceOffsetType.AnimationGroup);
                    //this.loadAnimationGroups(reader);

                    ////Unknown 6
                    //this.ValidatePointer(reader, ResourceOffsetType.Unknown6);
                    //this.loadUnknown6(reader);
                }

#if DEBUG_POINTER
                //EOF
                if (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{funcName}: EOF mismatch! ({reader.BaseStream.Length - reader.BaseStream.Position} bytes remaining) [File:{this.File.Name}]");
                    Console.ResetColor();
                }
#endif
                this.IsLoaded = true;
            }
        }

        private void loadCollision(BinaryReader reader)
        {
            var funcName = $"{nameof(ResourceFile)}->{Caller.GetMemberName()}";

            var collisionMeshPath = reader.ReadKoreanString();
            if (string.IsNullOrEmpty(collisionMeshPath) == false)
            {
                CollisionMesh = this.Load<MeshFile>(collisionMeshPath, ContentPurpose.Collision);
                if (CollisionMesh == null)
                    Console.WriteLine($"{funcName}: {nameof(CollisionMesh)} is null (Path:{collisionMeshPath}) [File:{this.File.Name}]");
            }

            BoundingBoxOuter = reader.ReadBoundingBox();
            BoundingBoxInner = reader.ReadBoundingBox();

            //TODO: Research extraBoundingBox
            var hasExtraBoundingBox = reader.ReadUInt32();
            if (hasExtraBoundingBox > 0)
            {
                reader.ReadBytes(64);
                //extraBoundingBox = new float[16];
                //for (int i = 0; i < extraBoundingBox.Length; i++)
                //{
                //    extraBoundingBox[i] = reader.ReadSingle();
                //    //Console.WriteLine(extraBoundingBox[i]);
                //}
            }
        }

        private void ValidatePointer(BinaryReader reader, ResourceOffsetType offsetType)
        {
#if DEBUG_POINTER
            if (reader.BaseStream.Position != _offsets[offsetType])
            {
                var funcName = $"{nameof(ResourceFile)}->{Caller.GetMemberName()}";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{funcName}: {offsetType} mismatch! ({reader.BaseStream.Position - _offsets[offsetType]} bytes) [File:{this.File.Name}]");
                Console.ResetColor();
            }
#endif
            reader.BaseStream.Position = _offsets[offsetType];
        }

        public override void Save(Stream writer)
        {
            var funcName = $"{nameof(ResourceFile)}->{Caller.GetMemberName()}";

            throw new NotImplementedException(funcName);
        }
    }
}