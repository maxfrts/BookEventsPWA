using System;

namespace bookEventsPWA.Models
{
    public class EventTypeModel: Enumeration
    {
        public static readonly EventTypeModel Cinema = new EventTypeModel(1, "Cinema");
        public static readonly EventTypeModel Show = new EventTypeModel(2, "Show");
        public static readonly EventTypeModel Palestra = new EventTypeModel(3, "Palestra");

       public EventTypeModel(int id, string name):base(id, name){}
    }
}