using System.Xml.Serialization;

namespace Addloc
{
    [XmlType("BookDescRoot")]
    public class ModBookDescRoot
    {
        public List<ModBookDesc> bookDescList = new();
    }
}
