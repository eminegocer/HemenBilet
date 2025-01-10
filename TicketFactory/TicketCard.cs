using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemenBiletProje.TicketFactory
{
    public class TicketCard : ITicketCard
    {
        public ITicketCard CreateTicketCard(string firstName, string lastName, string email, string Phone, string flightDetails)
        {
            // StandardTicketCard oluştur ve adapter ile dön.
            var standardCard = new StandardTicketCard(firstName, lastName, email, Phone, flightDetails);
            return new TicketCardAdapter(standardCard);
        }
    }
}
