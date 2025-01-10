using HemenBiletProje.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HemenBiletProje.Repositories
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetSeatsByFlightIdAsync(int flightId);
        Task<bool> ReserveSeatAsync(int seatId);
        Task<bool> IsSeatAvailableAsync(int seatId);
        Task<Seat> GetSeatByIdAsync(int seatId);
        Task AddSeatAsync(Seat seat);
        Task UpdateSeatAsync(Seat seat);
        Task InitializeDefaultReservationsAsync(int flightId);
        Task<string> GetFlightDetailsAsync(int flightId);
        Task SaveChangesAsync();
    }
}
