using System.Collections.Generic;
using bookEventsPWA.Models;

namespace bookEventsPWA.Services
{
    public interface IEventService
    {
        List<EventModel> GetAvailableEvents();
    }
}