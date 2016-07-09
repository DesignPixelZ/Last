using Last.Framework.Content.Data.Compound;
using Last.Framework.Content.Data.Resource;
using Last.Framework.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Last.Framework.Content.Common
{
    internal class ObjectIndexFile : ContentFile
    {
        private const string SUPPORTED_HEADER = "JMXVOBJI1000";

        private Dictionary<uint, ObjectIndexEntry> _entries;

        public IReadOnlyDictionary<uint, ObjectIndexEntry> Entries
        {
            get
            {
                return _entries;
            }
        }

        public ObjectIndexEntry this[uint key]
        {
            get
            {
                if (_entries.ContainsKey(key))
                    return _entries[key];

                return null;
            }
        }

        public ObjectIndexFile(ContentManager contentManager, FileInfo file)
            : base(contentManager, file)
        {
            _entries = new Dictionary<uint, ObjectIndexEntry>();
        }

        public override void Load(Stream stream, ContentPurpose purpose)
        {
            string funcName = $"{nameof(ObjectIndexFile)}->{Caller.GetMemberName()}";

            using (var reader = new StreamReader(stream))
            {
                this.Header = reader.ReadLine();
                if (this.Header != SUPPORTED_HEADER)
                {
                    Console.WriteLine($"{funcName}: Unsupported header detected! (Value = {this.Header}) [File:{this.File}]");
                }

                var entryCount = int.Parse(reader.ReadLine());
                for (int i = 0; i < entryCount; i++)
                {
                    var line = reader.ReadLine();
                    var data = Regex.Matches(line, @"[\""].+?[\""]|[^ ]+").Cast<Match>().Select(m => m.Value).ToArray();

                    var entry = new ObjectIndexEntry(data);
                    if (entry.Path.EndsWith(".bsr"))
                    {
                        entry.Resource = base.Load<ResourceFile>(entry.Path, purpose);
                    }
                    else if (entry.Path.EndsWith(".cpd"))
                    {
                        var compound = base.Load<CompoundFile>(entry.Path, purpose);
                        entry.Resource = compound?.CollisionResource;
                    }
                    _entries.Add(entry.ID, entry);
                }

                this.IsLoaded = true;
            }
        }

        public override void Save(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}