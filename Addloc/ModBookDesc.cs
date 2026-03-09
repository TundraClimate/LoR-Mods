using System.Xml.Serialization;

namespace Addloc
{
    [XmlType("BookDesc")]
    public class ModBookDesc
    {
        [XmlAttribute("Pid")]
        public string? pid;

        [XmlAttribute("BookID")]
        public int bookID;

        [XmlElement("BookName")]
        public string? bookName;

        [XmlArray("TextList")]
        [XmlArrayItem(typeof(string), ElementName = "Desc")]
        public List<string> texts = new();

        [XmlArray("PassiveList")]
        [XmlArrayItem(typeof(string), ElementName = "Passive")]
        public List<string> passives = new();
    }
}
