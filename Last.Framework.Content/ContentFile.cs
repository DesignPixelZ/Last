using System;
using System.IO;

namespace Last.Framework.Content
{
    public abstract class ContentFile
    {
        public ContentManager ContentManager { get; private set; }

        public FileInfo File { get; private set; }

        public bool IsLoaded { get; protected set; }

        protected char[] _header;

        public string Header
        {
            get { return new string(_header); }
            set { _header = value.ToCharArray(); }
        }

        public abstract void Load(Stream stream, ContentPurpose purpose);

        public abstract void Save(Stream stream);

        public ContentFile(ContentManager contentManager, FileInfo file)
        {
            this.ContentManager = contentManager;
            this.File = file;
        }

        protected T Load<T>(string internalPath, ContentPurpose purpose)
        {
            var resource = this.ContentManager.GetDataFile<T>(internalPath) as ContentFile;
            if (resource == null)
            {
                Console.WriteLine("ContentFile->Load: File not found (Path:{0}) [Container:{1}]", internalPath, this.File.Name);
                return default(T);
            }

            using (var resourceStream = resource.File.OpenRead())
            {
                resource.Load(resourceStream, purpose);
            }
            return (T)Convert.ChangeType(resource, typeof(T));
        }
    }
}