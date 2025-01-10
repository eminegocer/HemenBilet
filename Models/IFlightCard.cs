using HemenBiletProje.Models;

public interface IFlightCard
{
    Flight Flight { get; }
    string RenderHtml();
}