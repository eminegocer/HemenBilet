using HemenBiletProje.Models;

public interface IFlightCardFactory
{
    IFlightCard CreateFlightCard(Flight flight);
}