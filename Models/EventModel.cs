using System;

namespace bookEventsPWA.Models
{
    public class EventModel
    {
        public int EventId { get; set; }
        
        public EventTypeModel EventType { get; set; }

        public DateTime EventDate {get; set;}

        public string Description {get;set;}

        public string Summary {get; set;}

        public int TicketsForSale {get; set;}

        public LocationModel Location {get;set;}

        public double TicketPrice {get; set;}

        public string ThumbLocation {get; set;}
    }
}
