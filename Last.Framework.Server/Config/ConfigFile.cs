using System.Collections.Generic;
using System.Xml;

namespace Last.Framework.Server.Config
{
    public abstract class ConfigFile
    {
        private List<LoggerElement> _logger = new List<LoggerElement>();

        public XmlDocument ConfigDocument { get; private set; }   

        public IReadOnlyList<LoggerElement> Logger { get { return _logger; } }
        public BindingElement Binding { get; private set; }
        public SecurityElement Security { get; private set; }

        public ConfigFile(string fileName, string root)
        {
            this.ConfigDocument = new XmlDocument();
            this.ConfigDocument.Load(fileName);

            var rootNode = this.ConfigDocument[root];
            foreach (XmlNode node in rootNode)
            {
                if (node.NodeType != XmlNodeType.Element)
                    continue;

                switch (node.Name)
                {
                    case nameof(this.Logger):
                        var loggerElement = new LoggerElement(node);
                        _logger.Add(loggerElement);
                        break;

                    case nameof(this.Binding):
                        this.Binding = new BindingElement(node);
                        break;

                    case nameof(this.Security):
                        this.Security = new SecurityElement(node);
                        break;
                }
            }
        }
    }
}