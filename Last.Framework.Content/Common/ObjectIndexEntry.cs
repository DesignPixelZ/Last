using Last.Framework.Content.Data.Resource;

namespace Last.Framework.Content.Common
{
    public class ObjectIndexEntry
    {
        public uint ID { get; set; }
        public bool IsPassable { get; set; }
        public string Path { get; set; }

        public ResourceFile Resource { get; set; }

        public ObjectIndexEntry(string[] data)
        {
            this.ID = uint.Parse(data[0]);
            this.IsPassable = data[1] == "0x00000001";
            this.Path = data[2].Trim('\"');
        }
    }
}