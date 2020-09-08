using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Globalization;
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
                LocationModel location = new LocationModel() { Address = "Cinemark Eldorado - Av. Rebouças, 3970 - Pinheiros, SP", Latitude ="-23.572676", Longitude ="-46.695922"};
                LocationModel location2 = new LocationModel() { Address = "Multiplex PlayArte Marabá - Av. Ipiranga, 757 - República, SP", Latitude ="-23.542608", Longitude ="-46.640907"};
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
                                     Location = location2, 
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

        public List<EventModel> GetAvailableEventsByLocation(string userLatitude, string userLongitude)
        {
            return Events.Where(e => CheckDistance(e.Location.Latitude, e.Location.Longitude,userLatitude, userLongitude)).ToList();
        }

        private bool CheckDistance(string eventLatitute, string eventLongitude,string userLatitude, string userLongitude)
        {
            double eLat = double.Parse(eventLatitute, CultureInfo.InvariantCulture);
            double eLon = double.Parse(eventLongitude, CultureInfo.InvariantCulture);
            double uLat = double.Parse(userLatitude, CultureInfo.InvariantCulture);
            double uLon = double.Parse(userLongitude, CultureInfo.InvariantCulture);

            
            var d1 = eLat * (Math.PI / 180.0);
            var num1 = eLon * (Math.PI / 180.0);
            var d2 = uLat * (Math.PI / 180.0);
            var num2 = uLon * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            var distance =  6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
            if (distance < 10000.0){//Distance in meters
                return true;
            }

            return false;
        }
    }
}
