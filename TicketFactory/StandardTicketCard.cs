using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemenBiletProje.TicketFactory
{
    public class StandardTicketCard : IFlightTicketCard
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string FlightDetails { get; private set; }

        public StandardTicketCard(string firstName, string lastName, string email, string Phone, string flightDetails)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            FlightDetails = flightDetails;
        }
    }
}
