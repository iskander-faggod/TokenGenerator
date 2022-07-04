using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TokenGenerator.Services.Helper;

public static class ConvertXmlInModel
{
    public static string GetXmlData(DateTime dateTime)
    {
        var stringDate = dateTime.ToString("dd/MM/yyyy");
        var request =
            WebRequest.Create($"http://www.cbr.ru/scripts/XML_daily.asp?date_req={stringDate}") as HttpWebRequest;
        var response = request.GetResponse();

        Stream receiveStream = response.GetResponseStream();
        StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

        var result = readStream.ReadToEnd();
        return result;
    }

    public static T Deserialize<T>(string xmlText)
    {
        if (String.IsNullOrWhiteSpace(xmlText)) return default(T);

        using (StringReader stringReader = new System.IO.StringReader(xmlText))
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T) serializer.Deserialize(stringReader);
        }
    }
}