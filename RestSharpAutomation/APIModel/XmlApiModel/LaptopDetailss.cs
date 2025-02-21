using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestSharpLatest.APIModel.XmlApiModel
{
    [XmlRoot(ElementName = "laptopDetailss")]
    public class LaptopDetailss
    {
        [XmlElement(ElementName = "Laptop")]
        public List<Laptop> Laptop { get; set; }
    }
}
