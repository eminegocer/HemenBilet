using HemenBiletProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HemenBiletProje.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string CardNumber { get; set; } // Maksimum 16 karakter
        public string CardHolder { get; set; }   // Kart sahibinin adı (güncellendi)
        public int ExpirationMonth { get; set; } // Son kullanma tarihi (Ay)
        public int ExpirationYear { get; set; }  // Son kullanma tarihi (Yıl)
        public string CVV { get; set; }         // Maksimum 3 karakter

        public int SeatId { get; set; }         // Koltuk ID
        public int FlightId { get; set; }       // Uçuş ID
        public string PaymentMethod { get; set; } // Ödeme yöntemi (güncellendi)
        public Seat Seat { get; set; }          // Seçilen koltuk bilgisi
        public Flight Flight { get; set; }      // İlgili uçuş bilgisi
    }
}