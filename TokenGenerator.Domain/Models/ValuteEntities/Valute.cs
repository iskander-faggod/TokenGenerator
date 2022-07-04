using System.Xml.Serialization;

namespace TokenGenerator.Domain.Models;
[Serializable, XmlRoot(ElementName = "Valute")]
public class Valute
{
    [XmlAttribute("Id")]
    public string Id { get;  set; }
    
    [XmlElement(ElementName = "NumCode")]
    public int NumCode { get;  set; }
    
    [XmlElement(ElementName = "CharCode")]
    public string CharCode { get;  set; }
    
    [XmlElement(ElementName = "Name")]
    public string Name { get;  set; }
    
    [XmlElement(ElementName = "Value")]
    public string Value { get;  set; }
    
    [XmlElement(ElementName = "Nominal")]
    public int Nominal { get;  set; }
}