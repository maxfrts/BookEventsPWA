using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using bookEventsPWA.Models;

namespace bookEventsPWA.Services
{
    public class EventService : IEventService
    {
        private IWebHostEnvironment _env;
        public EventService(IWebHostEnvironment env)
        {
            _env = env;
        }

        //Lista de eventos simulados
        private List<EventModel> Events
        {
            get
            {
                EventTypeModel eventType = new EventTypeModel() { EventTypeId = 1, EventTypeDescription = "Cinema" };
                var eventDate = DateTime.Now.Date.AddDays(1);
                LocationModel location = new LocationModel() { Address = "Cinemark Eldorado - Av. Rebouças, 3970 - Pinheiros", Latitude ="", Longitude =""};
                return new List<EventModel>() {
                    new EventModel { EventId = 1, EventType = eventType, EventDate = eventDate, Description = "The Shining", Summary = "Reveja este clássico de Stanley Kubrick em versão estendida inédita nos cinemas brasileiros", TicketsForSale = 50, TicketPrice = 20, Location = location, ThumbLocation = "img/cinema/1-Poster.jpg" },
                    new EventModel { EventId = 2, EventType = eventType, EventDate = eventDate, Description = "The Godfather", Summary = "Reveja este clássico do cinema mundial em versão remasterizada.", TicketsForSale = 50, TicketPrice = 20, Location = location, ThumbLocation = "img/cinema/2-Poster.jpg"},
                    new EventModel { EventId = 3, EventType = eventType, EventDate = eventDate, Description = "The Godfather part II", Summary = "Reveja este clássico do cinema mundial em versão remasterizada.", TicketsForSale = 50, TicketPrice = 20, Location = location, ThumbLocation = "img/cinema/3-Poster.jpg"},
                    new EventModel { EventId = 4, EventType = eventType, EventDate = eventDate, Description = "Inglourious Basterds", Summary = "Nova restauração digital.", TicketsForSale = 50, TicketPrice = 20, Location = location, ThumbLocation = "img/cinema/4-Poster.jpg"}
                };
            }
        }

        public List<EventModel> GetAvailableEvents()
        {
            return Events.OrderByDescending(_ => _.EventId).ToList();
        }
    }
}
