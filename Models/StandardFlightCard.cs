
using HemenBiletProje.Models;

public class StandardFlightCard : IFlightCard
{
    public Flight Flight { get; private set; }

    public StandardFlightCard(Flight flight)
    {
        Flight = flight;
    }

    public string RenderHtml()
    {
        var html = $@"<div class='flight-item'>
            <div>
                <p><strong>Kalkış Yeri:</strong> {Flight.Airport}</p>
                <p><strong>Varış Yeri:</strong> {Flight.ArrivalAirport}</p>
                <p><strong>Uçuş Tarihi:</strong> {Flight.FlightDate}</p>
                <p><strong>Kalkış Saati:</strong> {Flight.Scheduled}</p>
                <p><strong>Uçuş Durumu:</strong> {Flight.FlightStatus}</p>
                <p class='flight-price'>Fiyat: {Flight.Price} ₺</p>
            </div>
            <div>
                <a href='/SeatSelection/SeatSelection?flightId={Flight.FlightId}' class='btn-primary'>
                    Bilet Al
                </a>
            </div>
        </div>";

        return html;
    }
}