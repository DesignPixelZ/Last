using Last.Framework.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace Last.Framework.Content.Data.NavMesh
{
    public class NavMeshFile : ContentFile
    {
        private const string SUPPORTED_HEADER = "JMXVNVM 1000";
        private const byte TEXTUREMAP_SIZE = 96;
        private const byte HEIGHTMAP_SIZE = 97;

        public List<NavMeshEntry> Entries;
        public List<NavMeshCell> Cells;
        public List<NavMeshRegionLink> RegionLinks;
        public List<NavMeshCellLink> CellLinks;
        public float[,] Heightmap;

        public NavMeshFile(ContentManager contentManager, FileInfo file) : base(contentManager, file)
        {
            this.Entries = new List<NavMeshEntry>();
            this.Cells = new List<NavMeshCell>();
            this.RegionLinks = new List<NavMeshRegionLink>();
            this.CellLinks = new List<NavMeshCellLink>();
            this.Heightmap = new float[HEIGHTMAP_SIZE, HEIGHTMAP_SIZE];
        }

        public override void Load(Stream stream, ContentPurpose purpose = ContentPurpose.Collision)
        {
            var funcName = $"{nameof(NavMeshFile)}->{Caller.GetMemberName()}";

            using (var reader = new BinaryReader(stream))
            {
                _header = reader.ReadChars(SUPPORTED_HEADER.Length);
                if (this.Header != SUPPORTED_HEADER)
                {
                    Console.WriteLine($"{funcName}: Unsupported header detected! (Value = {this.Header}) [File:{this.File.Name}]");
                    return;
                }

                //Read Entries
                var entryCount = reader.ReadUInt16();
                this.Entries = new List<NavMeshEntry>(entryCount);
                for (int entryIndex = 0; entryIndex < entryCount; entryIndex++)
                {
                    var entry = new NavMeshEntry(reader);
                    Entries.Add(entry);
                }

                //Read NavMeshCell
                var cellCount = reader.ReadUInt32();
                var cellExtraCount = reader.ReadUInt32(); //usage?
                for (int i = 0; i < cellCount; i++)
                {
                    var cell = new NavMeshCell(reader);
                    Cells.Add(cell);
                }

                //Read NavMeshRegionLinks
                var regionLinkCount = reader.ReadUInt32();
                for (int i = 0; i < regionLinkCount; i++)
                {
                    var link = new NavMeshRegionLink(reader);
                    RegionLinks.Add(link);
                }

                //Read NavMeshCellLinks
                var cellLinkCount = reader.ReadUInt32();
                for (int i = 0; i < cellLinkCount; i++)
                {
                    var link = new NavMeshCellLink(reader);
                    CellLinks.Add(link);
                }

                //Read TextureMap
                for (int x = 0; x < TEXTUREMAP_SIZE; x++)
                {
                    for (int y = 0; y < TEXTUREMAP_SIZE; y++)
                    {
                        var textureUShort0 = reader.ReadUInt16();
                        var textureUShort1 = reader.ReadUInt16();
                        var textureUShort2 = reader.ReadUInt16();
                        var textureUShort3 = reader.ReadUInt16();
                    }
                }

                //Read Height map
                for (int i = 0; i < HEIGHTMAP_SIZE * HEIGHTMAP_SIZE; i++)
                {
                    var height = reader.ReadSingle();
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

        public override void Save(Stream stream)
        {
            var funcName = $"{nameof(NavMeshFile)}->{Caller.GetMemberName()}";

            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(_header);

                //Write entries
                writer.Write((ushort)this.Entries.Count);
                for (int i = 0; i < this.Entries.Count; i++)
                {
                    this.Entries[i].Write(writer);
                }

                //Write Cells
                writer.Write((ushort)this.Cells.Count);
                for (int i = 0; i < this.Entries.Count; i++)
                {
                    this.Entries[i].Write(writer);
                }
            }
        }
    }
}