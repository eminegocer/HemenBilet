using HemenBiletProje.Models;

public class StandardFlightCardFactory : IFlightCardFactory
{
    public IFlightCard CreateFlightCard(Flight flight)
    {
        return new StandardFlightCard(flight);
    }
}