using System;
using System.Web.Mvc;
using HemenBiletProje.Models;
using HemenBiletProje.Entities;
using System.Linq;
using HemenBiletProje.Strategy;

namespace HemenBiletProje.Controllers
{
    public class PaymentController : Controller
    {
        private readonly FlightContext _context;

        public PaymentController()
        {
            _context = new FlightContext();
        }

        public ActionResult Payment(int flightId, int seatId)
        {
            TempData["FlightId"] = flightId;
            TempData["SeatId"] = seatId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessPayment(Payment payment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Payment");
                }

                // Ödeme stratejisini seç
                IPaymentStrategy paymentStrategy;

                if (payment.PaymentMethod == "visa")
                {
                    paymentStrategy = new VisaCardStrategy(payment.CardNumber, payment.CardHolder, payment.ExpirationMonth, payment.ExpirationYear, payment.CVV);
                }
                else if (payment.PaymentMethod == "mastercard")
                {
                    paymentStrategy = new MasterCardStrategy(payment.CardNumber, payment.CardHolder, payment.ExpirationMonth, payment.ExpirationYear, payment.CVV);
                }
                else if (payment.PaymentMethod == "amex")
                {
                    paymentStrategy = new AmexCardStrategy(payment.CardNumber, payment.CardHolder, payment.ExpirationMonth, payment.ExpirationYear, payment.CVV);
                }
                else
                {
                    throw new InvalidOperationException("Geçersiz ödeme yöntemi.");
                }

                // Ödeme işlemini gerçekleştir
                if (paymentStrategy.ProcessPayment(50.00m)) // Ödeme miktarını dinamik hale getirin
                {
                    TempData["SuccessMessage"] = "Ödeme başarılı!";
                    return RedirectToAction("Confirmation", "Payment");
                }
                else
                {
                    TempData["ErrorMessage"] = "Ödeme işlemi başarısız.";
                    return View("Payment");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ödeme işlemi sırasında bir hata oluştu: " + ex.Message;
                return View("Payment");
            }
        }

        public ActionResult Confirmation()
        {
            // TempData'dan gerekli bilgileri al
            var flightId = Convert.ToInt32(TempData["FlightId"]);
            var seatId = Convert.ToInt32(TempData["SeatId"]);

            if (flightId == null || seatId == null)
            {
                TempData["ErrorMessage"] = "Uçuş veya koltuk bilgisi bulunamadı.";
                return RedirectToAction("Payment");
            }

            Flight flight = _context.Flights.FirstOrDefault(f => f.FlightId == flightId);
            Seat seat = _context.Seats.FirstOrDefault(s => s.SeatId == seatId);


            if (flight == null || seat == null)
            {
                TempData["ErrorMessage"] = "Uçuş veya koltuk bilgisi bulunamadı.";
                return RedirectToAction("Payment");
            }

            // ViewModel oluştur
            var confirmationViewModel = new ConfirmationViewModel
            {
                FlightId = flight.FlightId,
                Airport = flight.Airport,
                ArrivalAirport = flight.ArrivalAirport,
                SeatNumber = seat.SeatNumber
            };

            return View(confirmationViewModel);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}