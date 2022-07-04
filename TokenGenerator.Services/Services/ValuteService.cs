using TokenGenerator.DataAccess.DataAccess;
using TokenGenerator.Domain.Models;
using TokenGenerator.Services.Helper;
using TokenGenerator.Services.Interfaces;

namespace TokenGenerator.Services.Services;

public class ValuteService : IValuteService
{
    public ValuteCurs GetValuteData(DateTime dateTime)
    {
        var xmlData = ConvertXmlInModel.GetXmlData(dateTime);
        ValuteCurs model = ConvertXmlInModel.Deserialize<ValuteCurs>(xmlData);
        return model;
    }
}