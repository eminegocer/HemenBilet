using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemenBiletProje.TicketFactory
{
    public class TicketCardAdapter : ITicketCard
    {
        private readonly StandardTicketCard _standardTicketCard;

        public TicketCardAdapter(StandardTicketCard standardTicketCard)
        {
            _standardTicketCard = standardTicketCard;
        }

        public ITicketCard CreateTicketCard(string firstName, string lastName, string email,string Phone, string flightDetails)
        {
            // Yeni bir StandardTicketCard oluşturuluyor ve bu bir adapter ile döndürülüyor.
            var standardCard = new StandardTicketCard(firstName, lastName, email,Phone, flightDetails);
            return new TicketCardAdapter(standardCard);
        }
    }
}


