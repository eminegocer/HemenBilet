using HemenBiletProje.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemenBiletProje.Models
{
    public class TicketCardViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int FlightId { get; set; }
        public int SeatId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Yeni Özellikler
        public Flight FlightDetails { get; set; }
        public Seat SeatDetails { get; set; }
    }
}

