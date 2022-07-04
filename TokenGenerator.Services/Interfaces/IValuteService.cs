using TokenGenerator.Domain.Models;

namespace TokenGenerator.Services.Interfaces;

public interface IValuteService
{
    ValuteCurs GetValuteData(DateTime dateTime);
}