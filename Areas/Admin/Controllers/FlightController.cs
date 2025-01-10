using System.Web.Mvc;


namespace HemenBiletProje.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    public class FlightController : Controller
    {
        public ActionResult FlightSearchPage()
        {
            return View();
        }

        // Uçak arama formundan gelen verileri işleyen action
        [HttpPost]
        public ActionResult SearchFlights(string from, string to, string departureDate, string returnDate, string tripType)
        {
            // Gelen verileri işleyin (örneğin: veritabanında uçuş arama)
            ViewBag.From = from;
            ViewBag.To = to;
            ViewBag.DepartureDate = departureDate;
            ViewBag.ReturnDate = returnDate;
            ViewBag.TripType = tripType;
            return View("SearchResults"); // "SearchResults.cshtml" adında bir view oluşturmanız gerekir
        }
    }
}