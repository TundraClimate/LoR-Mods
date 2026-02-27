using System.Xml.Serialization;
using System.Collections.Generic;

namespace Addloc
{
    [XmlType("BookDesc")]
    public class ModBookDesc
    {
        [XmlAttribute("Pid")]
        public string pid;

        [XmlAttribute("BookID")]
        public int bookID;

        // Token: 0x04004327 RID: 17191
        [XmlElement("BookName")]
        public string bookName;

        // Token: 0x04004328 RID: 17192
        [XmlArray("TextList")]
        [XmlArrayItem(typeof(string), ElementName = "Desc")]
        public List<string> texts;

        // Token: 0x04004329 RID: 17193
        [XmlArray("PassiveList")]
        [XmlArrayItem(typeof(string), ElementName = "Passive")]
        public List<string> passives;
    }
}
