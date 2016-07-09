using Last.Framework.Content.Data.Resource;
using Last.Framework.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Last.Framework.Content.Data.Compound
{
    public class CompoundFile : ContentFile
    {
        private const string SUPPORTED_HEADER = "JMXVCPD 0101";

        private CompoundOffsets _offsets;
        private ResourceFlags _flags;

        public ResourceType Type { get; private set; }
        public string Name { get; private set; }
        public uint unkUInt5;
        public uint unkUInt6;

        public ResourceFile CollisionResource { get; private set; }
        public List<ResourceFile> ResourceList { get; private set; }

        public CompoundFile(ContentManager contentManager, FileInfo file)
            : base(contentManager, file)
        {
            ResourceList = new List<ResourceFile>();
        }

        public override void Load(Stream stream, ContentPurpose purpose)
        {
            var funcName = $"{nameof(CompoundFile)}->{Caller.GetMemberName()}";

            using (var reader = new BinaryReader(stream))
            {
                _header = reader.ReadChars(SUPPORTED_HEADER.Length);
                if (this.Header != SUPPORTED_HEADER)
                {
                    Console.WriteLine($"{funcName}: Unsupported header detected! (Value = {this.Header}) [File:{this.File.Name}]");
                    return;
                }

                var pffsetBuffer = reader.ReadBytes(Marshal.SizeOf(_offsets));
                _offsets = Unmanaged.BufferToStruct<CompoundOffsets>(pffsetBuffer);

                var flagBuffer = reader.ReadBytes(Marshal.SizeOf(_flags));
                _flags = Unmanaged.BufferToStruct<ResourceFlags>(pffsetBuffer);

                Type = (ResourceType)reader.ReadUInt32();

                Name = reader.ReadKoreanString();
                unkUInt5 = reader.ReadUInt32();
                unkUInt6 = reader.ReadUInt32();

#if DEBUG_CONTENT
                if (unkUInt5 != 0)
                    Console.WriteLine($"{funcName}: {nameof(unkUInt5)} != 0 (Value = {unkUInt5}) [File:{this.File.Name}]");
                if (unkUInt6 != 0)
                    Console.WriteLine($"{funcName}: {nameof(unkUInt6)} != 0 (Value = {unkUInt6}) [File:{this.File.Name}]");
#endif

                //CollisionResource
                if (purpose.HasFlags(ContentPurpose.Collision))
                {
                    this.ValidatePointer(reader, CompoundOffsetType.CollisionResource);
                    this.loadCollisionResource(reader);
                }

                //ResourceList
                if (purpose.HasFlags(ContentPurpose.Visual))
                {
                    throw new NotImplementedException();

                    //this.ValidatePointer(reader, CompoundOffsetType.ResourceList);
                    //this.loadResourceList(reader);
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

                this.IsLoaded = true;
            }
        }

        private void loadCollisionResource(BinaryReader reader)
        {
            var funcName = $"{nameof(CompoundFile)}->{Caller.GetMemberName()}";
            reader.BaseStream.Position = _offsets[CompoundOffsetType.CollisionResource];

            string collisionResourcePath = reader.ReadKoreanString();
            if (string.IsNullOrEmpty(collisionResourcePath) == false)
            {
                CollisionResource = base.Load<ResourceFile>(collisionResourcePath, ContentPurpose.Collision);
                if (CollisionResource == null)
                    Console.WriteLine($"{funcName}: {nameof(CollisionResource)} is null (Path:{collisionResourcePath}) [File:{this.File.Name}]");
            }
        }

        private void ValidatePointer(BinaryReader reader, CompoundOffsetType offsetType)
        {
#if DEBUG_POINTER
            if (reader.BaseStream.Position != _offsets[offsetType])
            {
                var funcName = $"{nameof(CompoundFile)}->{Caller.GetMemberName()}";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{funcName}: {offsetType} mismatch! ({reader.BaseStream.Position - _offsets[offsetType]} bytes) [File:{this.File.Name}]");
                Console.ResetColor();
            }
#endif
            reader.BaseStream.Position = _offsets[offsetType];
        }

        public override void Save(Stream writer)
        {
            throw new NotImplementedException();
        }
    }
}