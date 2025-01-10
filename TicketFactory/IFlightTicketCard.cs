using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemenBiletProje.TicketFactory
{
    public interface IFlightTicketCard
    {
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string Phone { get; }
        string FlightDetails { get; }
    }
}


