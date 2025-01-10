using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HemenBiletProje.Entities;

namespace HemenBiletProje.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightId { get; set; }
        
        // API'den gelen özellikler
        public string FlightDate { get; set; }
        public string FlightStatus { get; set; }
        public string Airport { get; set; }
        public string ArrivalAirport { get; set; }
        public string Scheduled { get; set; }



        // Bizim ekleyeceğimiz özellikler
        [NotMapped]
        public string DepartureCity 
        { 
            get { return Airport; } 
            set { Airport = value; }
        }
        
        [NotMapped]
        public string ArrivalCity 
        { 
            get { return ArrivalAirport; } 
            set { ArrivalAirport = value; }
        }
        
        [NotMapped]
        public decimal Price { get; set; } = 200;
        
        [NotMapped]
        public string DepartureTime 
        { 
            get { return Scheduled; } 
            set { Scheduled = value; }
        }

        // Navigation property
        public virtual ICollection<Seat> Seats { get; set; }

        //public virtual ICollection<TicketCard> TicketCards { get; set; }
        public Flight()
        {
            Seats = new List<Seat>();
            Price = 200;
        }
    }
}
