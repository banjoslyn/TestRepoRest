﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RestSharpLatest.APIModel.XmlApiModel
{
    [XmlRoot(ElementName = "Features")]
    public class Features
    {
        [XmlElement(ElementName = "Feature")]
        public List<string> Feature { get; set; }
    }
}

