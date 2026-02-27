using System.Xml.Serialization;
using System.Collections.Generic;

namespace Addloc
{
    [XmlType("BookDescRoot")]
    public class ModBookDescRoot
    {
        public List<ModBookDesc> bookDescList;
    }
}
