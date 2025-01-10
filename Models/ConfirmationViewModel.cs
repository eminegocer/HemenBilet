using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemenBiletProje.Models
{
    public class ConfirmationViewModel
    {
        public int FlightId { get; set; }
        public string Airport { get; set; }
        public string ArrivalAirport { get; set; }
        public string SeatNumber { get; set; }
    }

}
