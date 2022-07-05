using System.Xml.Serialization;
using TokenGenerator.Domain.Models.ValuteEntities;

namespace TokenGenerator.Domain.Models;

[Serializable, XmlRoot(ElementName = "ValCurs")]
public class ValuteCurs
{
    [XmlAttribute("Date")]
    public string Date { get;  set; }
    [XmlAttribute("name")]
    public string Name { get;  set; }

    [XmlElement(ElementName = "Valute")] 
    public List<Valute> Valutes { get;  set; }
}