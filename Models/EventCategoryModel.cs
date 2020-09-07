using System;

namespace bookEventsPWA.Models
{
    public class EventCategoryModel: Enumeration
    {
        public static readonly EventCategoryModel Acao = new EventCategoryModel(1, "Ação");
        public static readonly EventCategoryModel Aventura = new EventCategoryModel(2, "Aventura");
        public static readonly EventCategoryModel Fantasia = new EventCategoryModel(3, "Fantasia");
        public static readonly EventCategoryModel Tecnologia = new EventCategoryModel(4, "Tecnologia");
        public static readonly EventCategoryModel Drama = new EventCategoryModel(5, "Drama");
        public static readonly EventCategoryModel Comedia = new EventCategoryModel(6, "Comédia");
        public static readonly EventCategoryModel Terror = new EventCategoryModel(7, "Terror");
        public static readonly EventCategoryModel Suspense = new EventCategoryModel(8, "Suspense");
        public static readonly EventCategoryModel Romance = new EventCategoryModel(9, "Romance");
        public static readonly EventCategoryModel Rock = new EventCategoryModel(10, "Rock");
        public static readonly EventCategoryModel Eletronica = new EventCategoryModel(11, "Eletronica");
        public static readonly EventCategoryModel Sertanejo = new EventCategoryModel(12, "Sertanejo");
        public static readonly EventCategoryModel Samba = new EventCategoryModel(13, "Samba");
        public static readonly EventCategoryModel Classica = new EventCategoryModel(14, "Classica");

       public EventCategoryModel(int id, string name):base(id, name){}
    }
}