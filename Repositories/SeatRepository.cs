using HemenBiletProje.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HemenBiletProje.Models;
using System.Data.Entity;
using System;

namespace HemenBiletProje.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly FlightContext _context;

        public SeatRepository(FlightContext context)
        {
            _context = context;
        }

        // U�u�a ait t�m koltuklar� getir
        public async Task<IEnumerable<Seat>> GetSeatsByFlightIdAsync(int flightId)
        {
            // Koltuk numaralarını A1'den D6'ya kadar tanımla
            var seatNumbers = new List<string>
        {
            "A1", "A2", "A3", "A4", "A5", "A6",
            "B1", "B2", "B3", "B4", "B5", "B6",
            "C1", "C2", "C3", "C4", "C5", "C6",
            "D1", "D2", "D3", "D4", "D5", "D6"
        };

            // Veritabanından uçuşa ait mevcut koltuk bilgilerini getir
            var existingSeats = await _context.Seats
                .Where(s => s.FlightId == flightId)
                .OrderBy(s => s.SeatNumber)
                .ToListAsync();

            // Eğer mevcut koltuk bilgisi yoksa yeni koltuk bilgileri oluştur
            if (!existingSeats.Any())
            {
                var newSeats = seatNumbers.Select((seatNumber, index) => new Seat
                {
                    SeatId = index + 1, // Koltuk numarası ile id eşlemesi (A1 için 1, A2 için 2, ...)
                    SeatNumber = seatNumber,
                    IsReserved = false,
                    FlightId = flightId,
                    UserId = null // Kullanıcı bilgisi null
                }).ToList();

                // Yeni koltukları veritabanına ekle
                _context.Seats.AddRange(newSeats);
                await _context.SaveChangesAsync();

                return newSeats; // Bu asenkron bir yapı değildir, doğrudan dönebilirsiniz
            }

            // Eğer mevcut koltuklar varsa bunları döndür
            return existingSeats;
        }



        // Varsay�lan olarak baz� koltuklar� rezerve et
        public async Task InitializeDefaultReservationsAsync(int flightId)
        {
            // Uçuşa ait tüm koltukları getir
            var seats = await _context.Seats
                .Where(s => s.FlightId == flightId)
                .ToListAsync();

            // Zaten rezerve edilmiş koltukların sayısını hesapla
            var reservedSeatsCount = seats.Count(s => s.IsReserved);

            // 5 koltuğun rezerve edilmesi için gereken koltuk sayısını hesapla
            var seatsToReserveCount = 5 - reservedSeatsCount;

            // Eğer veritabanında zaten 5 koltuk rezerve edilmişse, işlemi sonlandır
            if (reservedSeatsCount >= 5)
            {
                return; // 5'ten fazla rezerve edilmiş koltuk varsa işlem yapılmaz
            }

            // Eğer rezerve edilecek koltuk kalmamışsa işlemi sonlandır
            if (seatsToReserveCount > 0)
            {
                // Rezerve edilmemiş koltukları rastgele sırala ve gerekli sayıda seç
                var availableSeats = seats
                    .Where(s => !s.IsReserved)
                    .OrderBy(x => Guid.NewGuid()) // Rastgele sıralama
                    .Take(seatsToReserveCount)
                    .ToList();

                // Rezerve edilecek koltukları işaretle
                foreach (var seat in availableSeats)
                {
                    seat.IsReserved = true;
                }

                // Değişiklikleri veritabanına kaydet
                await _context.SaveChangesAsync();
            }
        }



        // Belirli bir koltu�u rezerve et
        public async Task<bool> ReserveSeatAsync(int seatId)
        {
            var seat = await _context.Seats.FindAsync(seatId);

            if (seat == null || seat.IsReserved)
                return false; // Koltuk bulunamad� veya zaten rezerve edilmi�

            seat.IsReserved = true;

            try
            {
                await _context.SaveChangesAsync();
                return true; // Ba�ar�l� rezervasyon
            }
            catch
            {
                return false; // Hata durumunda false d�nd�r
            }
        }

        // Belirli bir koltu�un uygun olup olmad���n� kontrol et
        public async Task<bool> IsSeatAvailableAsync(int seatId)
        {
            var seat = await _context.Seats.FindAsync(seatId);
            return seat != null && !seat.IsReserved;
        }


        public async Task<string> GetFlightDetailsAsync(int flightId)
        {
            // Uçuş detaylarını veritabanından al
            var flight = await _context.Flights.FindAsync(flightId);

            if (flight == null)
            {
                return null; // Uçuş bulunamadıysa null döndür
            }

            // Uçuş detaylarını bir string olarak formatla
            var flightDetails = $"Flight ID: {flight.FlightId}, Date: {flight.FlightDate}, " +
                                $"Status: {flight.FlightStatus}, Departure: {flight.Airport}, " +
                                $"Scheduled: {flight.Scheduled}, Arrival: {flight.ArrivalAirport}";

            return flightDetails;
        }



        // Belirli bir koltu�u getir
        public async Task<Seat> GetSeatByIdAsync(int seatId)
        {
            return await _context.Seats.FindAsync(seatId);
        }

        // Yeni bir koltuk ekle
        public async Task AddSeatAsync(Seat seat)
        {
            _context.Seats.Add(seat);
            await _context.SaveChangesAsync();
        }

        // Mevcut bir koltu�u g�ncelle
        public async Task UpdateSeatAsync(Seat seat)
        {
            var existingSeat = await _context.Seats.FindAsync(seat.SeatId);
            if (existingSeat == null) return;

            existingSeat.SeatNumber = seat.SeatNumber;
            existingSeat.IsReserved = seat.IsReserved;
            existingSeat.FlightId = seat.FlightId;

            await _context.SaveChangesAsync();
        }

        // De�i�iklikleri kaydet
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
