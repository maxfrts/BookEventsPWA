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
                var eventDate = DateTime.Now.Date.AddDays(1);
                LocationModel location = new LocationModel() { Address = "Cinemark Eldorado - Av. Rebouças, 3970 - Pinheiros", Latitude ="", Longitude =""};
                return new List<EventModel>() {
                    //The shining
                    new EventModel { EventId = 1,
                                     EventType = EventTypeModel.Cinema,
                                     CategoryList = new List<EventCategoryModel>(){ EventCategoryModel.Suspense },
                                     EventDate = eventDate,
                                     Description = "The Shining",
                                     Summary = "Reveja este clássico de Stanley Kubrick em versão estendida inédita nos cinemas brasileiros",
                                     TicketsForSale = 50,
                                     TicketPrice = 20,
                                     Location = location,
                                     ThumbLocation = "img/cinema/1-Poster.jpg",
                                     Duration = 146},
                    //The Godfather
                    new EventModel { EventId = 2, 
                                     EventType = EventTypeModel.Cinema,
                                     CategoryList = new List<EventCategoryModel>(){EventCategoryModel.Acao, EventCategoryModel.Drama},
                                     EventDate = eventDate,
                                     Description = "The Godfather",
                                     Summary = "Reveja este clássico do cinema mundial em versão remasterizada.", 
                                     TicketsForSale = 50, 
                                     TicketPrice = 20, 
                                     Location = location,
                                     ThumbLocation = "img/cinema/2-Poster.jpg",
                                     Duration = 178},
                    //The Godfather part II
                    new EventModel { EventId = 3, 
                                     EventType = EventTypeModel.Cinema, 
                                     CategoryList = new List<EventCategoryModel>(){EventCategoryModel.Acao, EventCategoryModel.Drama},
                                     EventDate = eventDate, 
                                     Description = "The Godfather part II", 
                                     Summary = "Reveja este clássico do cinema mundial em versão remasterizada.", 
                                     TicketsForSale = 50, 
                                     TicketPrice = 20, 
                                     Location = location, 
                                     ThumbLocation = "img/cinema/3-Poster.jpg",
                                     Duration = 202},
                    //Inglourious Basterds
                    new EventModel { EventId = 4, 
                                     EventType = EventTypeModel.Cinema, 
                                     CategoryList = new List<EventCategoryModel>(){EventCategoryModel.Acao, EventCategoryModel.Aventura},
                                     EventDate = eventDate, 
                                     Description = "Inglourious Basterds", 
                                     Summary = "Nova restauração digital.", 
                                     TicketsForSale = 50, 
                                     TicketPrice = 20, 
                                     Location = location, 
                                     ThumbLocation = "img/cinema/4-Poster.jpg",
                                     Duration = 153}
                };
            }
        }

        public List<EventModel> GetAvailableEvents()
        {
            return Events.OrderByDescending(_ => _.EventId).ToList();
        }

        public List<EventModel> GetAvailableEventsByType(EventTypeModel type)
        {
            return Events.Where(e => e.EventType == type).OrderByDescending(_ => _.EventId).ToList();
        }

        public List<EventModel> GetAvailableEventsByCategory(EventCategoryModel category)
        {
            return Events.Where(e => e.CategoryList.Contains(category)).OrderByDescending(_ => _.EventId).ToList();
        }

        public List<EventModel> GetAvailableEventsByTypeAndCategory(EventTypeModel type, EventCategoryModel category)
        {
            return Events.Where(e => e.EventType == type && e.CategoryList.Contains(category)).OrderByDescending(_ => _.EventId).ToList();
        }

        public EventModel GetEventById(int id){
            return Events.Where(e => e.EventId == id).FirstOrDefault();
        }


    }
}
