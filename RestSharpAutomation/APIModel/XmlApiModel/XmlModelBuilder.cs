using RestSharpLatest.APIModel.JsonApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RestSharpLatest.APIModel.XmlApiModel
{
    public class XmlModelBuilder
    { 
            private string _BrandName { get; set; }
    private Features _Features { get; set; }
    private string _Id { get; set; }
    private string _LaptopName { get; set; }

    public Laptop Build()
    {
        return new Laptop()
        {
            BrandName = _BrandName,
            Features = _Features,
            Id = _Id,
            LaptopName = _LaptopName
        };
    }

    public XmlModelBuilder WithBrandName(string name)
    {
        _BrandName = name;
        return this;
    }

    public XmlModelBuilder WithId(string id)
    {
        _Id = id;
        return this;
    }

    public XmlModelBuilder WithLaptopName(string name)
    {
        _LaptopName = name;
        return this;
    }

    public XmlModelBuilder WithFeatures(List<string> feature)
    {
        _Features = new Features()
        {
            Feature = feature
        };
        return this;
    }



    }
}
