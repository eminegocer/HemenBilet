using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemenBiletProje.TicketFactory
{
    public interface ITicketCard
    {
        ITicketCard CreateTicketCard(string firstName, string lastName, string email,string Phone, string flightDetails);
    }
}



