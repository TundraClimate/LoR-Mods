using System.Xml.Serialization;

namespace Addloc
{
    [XmlType("CharactersNameRoot")]
    public class ModCharactersNameRoot
    {
        [XmlElement("Name")]
        public List<ModCharacterName> nameList = new();
    }
}
