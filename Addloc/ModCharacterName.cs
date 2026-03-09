using System.Xml.Serialization;

namespace Addloc
{
    [XmlType("Name")]
    public class ModCharacterName
    {
        [XmlAttribute("Pid")]
        public string? pid;

        [XmlAttribute("ID")]
        public int ID;

        [XmlText]
        public string? name;
    }
}
