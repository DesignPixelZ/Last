using NLog;
using System.Xml;

namespace Last.Framework.Server.Config
{
    public class LoggerElement
    {
        public string Name { get; set; }
        public LogLevel Level { get; set; }

        public LoggerElement(XmlNode node)
        {
            this.Name = node.Attributes[nameof(this.Name)].Value;
            this.Level = LogLevel.FromString(node.Attributes[nameof(this.Level)].Value);
        }
    }
}