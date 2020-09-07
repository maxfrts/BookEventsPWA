using System.Collections.Generic;
using bookEventsPWA.Models;

namespace bookEventsPWA.Services
{
    public interface IEventService
    {
        List<EventModel> GetAvailableEvents();
        List<EventModel> GetAvailableEventsByTypeAndCategory(EventTypeModel type, EventCategoryModel category);
        List<EventModel> GetAvailableEventsByCategory(EventCategoryModel category);
        List<EventModel> GetAvailableEventsByType(EventTypeModel type);
        EventModel GetEventById(int id);
    }
}