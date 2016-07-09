using System;
using System.Collections.Generic;
using System.IO;

namespace Last.Framework.Content
{
    public class ContentManager
    {
        private const string Data = "Data";
        private const string Map = "Map";
        private const string Media = "Media";
        private const string Music = "Music";
        private const string Particle = "Particle";

        private Dictionary<FileInfo, ContentFile> _cache;

        private ContentSource _source;
        private string _path;

        public ContentManager(ContentSource source, string path)
        {
            _source = source;
            _path = path;

            _cache = new Dictionary<FileInfo, ContentFile>();
        }

        public void Clear()
        {
            _cache.Clear();
        }

        public T GetDataFile<T>(string internalPath)
        {
            return this.GetFile<T>(Data, internalPath);
        }

        public T GetMapFile<T>(string internalPath)
        {
            return this.GetFile<T>(Map, internalPath);
        }

        public T GetMediaFile<T>(string internalPath)
        {
            return this.GetFile<T>(Media, internalPath);
        }

        public T GetMusicFile<T>(string internalPath)
        {
            return this.GetFile<T>(Music, internalPath);
        }

        public T GetParticleFile<T>(string internalPath)
        {
            return this.GetFile<T>(Particle, internalPath);
        }

        internal T GetFile<T>(string filter, string internalPath)
        {
            var file = new FileInfo(Path.Combine(_path, filter, internalPath));
            if (_cache.ContainsKey(file))
            {
                return (T)Convert.ChangeType(_cache[file], typeof(T));
            }
            else
            {
                if (file.Exists)
                {
                    var contentFile = Activator.CreateInstance(typeof(T), this, file);
                    //using (var stream = file.OpenRead())
                    //{
                    //    (contentFile as ContentFile).Load(stream, ContentPurpose.Any);
                    //}
                    _cache.Add(file, contentFile as ContentFile);
                    return (T)contentFile;
                }
            }
            return default(T);
        }
    }
}