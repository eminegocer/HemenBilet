using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using Newtonsoft.Json;
using HemenBiletProje.Models;
using HemenBiletProje.Entities;

namespace HemenBiletProje.Controllers
{
    public class FlightController : Controller
    {
        private readonly string _apiUrl = "https://api.aviationstack.com/v1/flights";
        private readonly string _apiKey = "5e7ec63b75ff4fad01b0a2e0526c5bf9";
        private readonly string _connectionString = "Server=EMINEGOCER\\SQLEXPRESS01;Database=Flights;Trusted_Connection=True;";
        private readonly FlightContext db;
        private readonly IFlightCardFactory _flightCardFactory;

        public FlightController()
        {
            db = new FlightContext();
            _flightCardFactory = new StandardFlightCardFactory();
        }

        // API'den veri çekme ve kaydetme
        public async Task<ActionResult> Index()
        {
            var flights = await GetFlightDataAsync();
            int rowsInserted = SaveFlightsToDatabase(flights);
            ViewBag.Message = $"{rowsInserted} adet uçuş verisi başarıyla kaydedildi.";
            return View(flights);
        }

        private async Task<List<Flight>> GetFlightDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{_apiUrl}?access_key={_apiKey}";
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);
                    return apiResponse?.Data ?? new List<Flight>();
                }
                return new List<Flight>();
            }
        }

        private int SaveFlightsToDatabase(List<Flight> flights)
        {
            int rowsInserted = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var flight in flights)
                {
                    var command = new SqlCommand(
                        "INSERT INTO Flights (FlightDate, FlightStatus, Airport, Scheduled) " +
                        "VALUES (@FlightDate, @FlightStatus, @Airport, @Scheduled)",
                        connection);

                    command.Parameters.AddWithValue("@FlightDate", flight.FlightDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@FlightStatus", flight.FlightStatus ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Airport", flight.Airport ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Scheduled", flight.Scheduled ?? (object)DBNull.Value);

                    rowsInserted += command.ExecuteNonQuery();
                }
            }
            return rowsInserted;
        }

        private void CreateSeatsForFlight(int flightId, SqlConnection connection)
        {
            string[] rows = { "A", "B", "C", "D" };
            foreach (var row in rows)
            {
                for (int i = 1; i <= 6; i++)
                {
                    var seatNumber = $"{row}{i}";
                    var command = new SqlCommand(
                        @"INSERT INTO Seats (SeatNumber, IsReserved, FlightId) 
                          VALUES (@SeatNumber, 0, @FlightId)",
                        connection);

                    command.Parameters.AddWithValue("@SeatNumber", seatNumber);
                    command.Parameters.AddWithValue("@FlightId", flightId);

                    command.ExecuteNonQuery();
                }
            }
        }
        [HttpGet]
        // Uçuş Arama Sayfası
        public ActionResult FlightSearchPage()
        {
            // GET isteğinde ViewBag'leri doldur
            ViewBag.DepartureAirports = db.Flights
                .Where(f => !string.IsNullOrEmpty(f.Airport))
                .Select(f => f.Airport)
                .Distinct()
                .ToList();

            ViewBag.ArrivalAirports = db.Flights
                .Where(f => !string.IsNullOrEmpty(f.ArrivalAirport))
                .Select(f => f.ArrivalAirport)
                .Distinct()
                .ToList();

            return View();
        }


        // Uçuş Arama İşlemi
        [HttpPost]

        public ActionResult FlightSearchPage(string from, string to, DateTime departureDate)
        {
            try
            {
                // Önce ViewBag'leri doldur
                ViewBag.DepartureAirports = db.Flights
                    .Where(f => !string.IsNullOrEmpty(f.Airport))
                    .Select(f => f.Airport)
                    .Distinct()
                    .ToList();

                ViewBag.ArrivalAirports = db.Flights
                    .Where(f => !string.IsNullOrEmpty(f.ArrivalAirport))
                    .Select(f => f.ArrivalAirport)
                    .Distinct()
                    .ToList();

                // Uçuş arama işlemi
                var flights = db.Flights
                    .AsEnumerable()
                    .Where(f => f.Airport.Equals(from, StringComparison.OrdinalIgnoreCase) &&
                               f.ArrivalAirport.Equals(to, StringComparison.OrdinalIgnoreCase) &&
                               DateTime.Parse(f.FlightDate).Date == departureDate.Date)
                    .ToList();

                if (!flights.Any())
                {
                    // Sadece ViewBag.ErrorMessage kullan, TempData kullanma
                    ViewBag.ErrorMessage = "Aradığınız kriterlere uygun uçuş bulunamadı.";
                    return View();
                }

                // Uçuşlar bulundu
                TempData["FilteredFlights"] = flights;
                return RedirectToAction("FlightListPage");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Uçuş arama sırasında bir hata oluştu: " + ex.Message;
                return View();
            }
        }

        // Uçuş Listesi Sayfası
        public ActionResult FlightListPage()
        {
            var flights = TempData["FilteredFlights"] as List<Flight>;
            if (flights == null || !flights.Any())
            {
                ViewBag.DepartureAirports = db.Flights
                    .Where(f => !string.IsNullOrEmpty(f.Airport))
                    .Select(f => f.Airport)
                    .Distinct()
                    .ToList();

                ViewBag.ArrivalAirports = db.Flights
                    .Where(f => !string.IsNullOrEmpty(f.ArrivalAirport))
                    .Select(f => f.ArrivalAirport)
                    .Distinct()
                    .ToList();

                ViewBag.ErrorMessage = "Aradığınız kriterlere uygun uçuş bulunamadı.";
                return View("FlightSearchPage");
            }
            return View(flights);
        }




        // Uçuş Listesi Sayfası
        public ActionResult FlightList()
        {
            var flights = TempData["Flights"] as List<Flight>;
            if (flights == null)
            {
                return RedirectToAction("FlightSearchPage");
            }
            return View(flights);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    // API Response Model
    public class ApiResponse
    {
        [JsonProperty("data")]
        public List<Flight> Data { get; set; }
    }
   
}
