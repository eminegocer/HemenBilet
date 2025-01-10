using System.Web.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HemenBiletProje.Repositories;
using HemenBiletProje.Entities;
using HemenBiletProje.Models;
using HemenBiletProje.TicketFactory;
using System;
using System.Data.Entity;

public class SeatSelectionController : Controller
{
    private readonly ISeatRepository _seatRepository;
    private readonly FlightContext _context;

    public SeatSelectionController(ISeatRepository seatRepository, FlightContext context)
    {
        _context = context;
        _seatRepository = seatRepository;
    }

    // Uçuşa ait koltukları getir ve varsayılan rezervasyonları başlat
    public async Task<ActionResult> SeatSelection(int? flightId)
    {
        if (!flightId.HasValue)
        {
            TempData["ErrorMessage"] = "Flight ID is required.";
            return RedirectToAction("Index", "Flight");
        }

        // Varsayılan rezervasyonları başlat
        await _seatRepository.InitializeDefaultReservationsAsync(flightId.Value);

        var seats = await _seatRepository.GetSeatsByFlightIdAsync(flightId.Value);
        ViewBag.FlightId = flightId;
        return View(seats);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ReserveSeat(int? seatId, int? flightId)
    {
        if (!seatId.HasValue || !flightId.HasValue)
        {
            TempData["ErrorMessage"] = "Please select a seat and provide a flight ID.";
            return RedirectToAction("SeatSelection", new { flightId });
        }

        var success = await _seatRepository.ReserveSeatAsync(seatId.Value);
        if (success)
        {
            TempData["SuccessMessage"] = "Seat successfully reserved.";
            return RedirectToAction("TicketCard", new { flightId, seatId });
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to reserve the seat. It might already be reserved.";
            return RedirectToAction("SeatSelection", new { flightId });
        }
    }


    public ActionResult TicketCard(int flightId, int seatId)
    {
        ViewBag.FlightId = flightId;
        ViewBag.SeatId = seatId;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult TicketCard(TicketCardViewModel model)
    {
        try
        {
            // Flight ve Seat detaylarını al
            var flight = _context.Flights.Find(model.FlightId);
            var seat = _context.Seats.Find(model.SeatId);

            // ViewModel'e bilgileri ekle
            model.FlightDetails = flight;
            model.SeatDetails = seat;

            // Ticket Factory kullanarak kart oluştur
            ITicketCard ticketFactory = new HemenBiletProje.TicketFactory.TicketCard();
            var ticket = ticketFactory.CreateTicketCard(
                model.FirstName,
                model.LastName,
                model.Email,
                model.PhoneNumber,
                $"{flight.Airport} to {flight.ArrivalAirport}, Date: {flight.FlightDate}, Seat: {seat.SeatNumber}"
            );

            // Veritabanı kaydı için TicketCard entity'si oluştur
            var ticketCardEntity = new Ticket
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FlightId = model.FlightId,
                SeatId = model.SeatId,
                CreatedAt = DateTime.Now
            };

            // Veritabanına kaydet
            _context.TicketCards.Add(ticketCardEntity);
            _context.SaveChanges();

            // Kart görüntüleme view'ına yönlendir
            return View("TicketCardDisplay", model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Bilet oluşturulurken bir hata oluştu: " + ex.Message;
            return RedirectToAction("TicketInformation");
        }
    }
}


