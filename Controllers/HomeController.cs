using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using bookEventsPWA.Models;
using bookEventsPWA.Services;
using Lib.Net.Http.WebPush;

namespace bookEventsPWA.Controllers
{
    public class HomeController : Controller
    {
        private IEventService _eventService;
        private readonly IPushService _pushService;
        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult AvailableEvents()
        {
            var events = _eventService.GetAvailableEvents();
            return Json(events);
        }

        public JsonResult AvailableEventsByLocation(string userLocation)
        {
            var events = _eventService.GetAvailableEvents();
            return Json(events);
        }

        public JsonResult LoadEvent(int id)
        {
            var evento = _eventService.GetEventById(id);
            return Json(evento);
        }

        [HttpGet("publickey")]
        public ContentResult GetPublicKey()
        {
            return Content(_pushService.GetKey(), "text/plain");
        }

        //armazena subscricoes
        [HttpPost("subscriptions")]
        public async Task<IActionResult> StoreSubscription([FromBody]PushSubscription subscription)
        {
            int res = await _pushService.StoreSubscriptionAsync(subscription);

            if (res > 0)
                return CreatedAtAction(nameof(StoreSubscription), subscription);

            return NoContent();
        }

        [HttpDelete("subscriptions")]
        public async Task<IActionResult> DiscardSubscription(string endpoint)
        {
            await _pushService.DiscardSubscriptionAsync(endpoint);

            return NoContent();
        }

        [HttpPost("notifications")]
        public async Task<IActionResult> SendNotification([FromBody]PushMessageViewModel messageVM)
        {
            var message = new PushMessage(messageVM.Notification)
            {
                Topic = messageVM.Topic,
                Urgency = messageVM.Urgency                
            };

            _pushService.SendNotificationAsync(message);

            return NoContent();
        }

        // private bool CheckDistance(string eventLatitute, string eventLongitude,string userLatitude,string userLongitude, int metros){
        //     var uCoord = new GeoCoordinate(userLatitude, userLongitude);
        //     var eCoord = new GeoCoordinate(eventLatitute, eventLongitude);

        //     var distance = uCoord.GetDistanceTo(eCoord);
        //     if(distance < 5000){
        //         return true;
        //     } else{
        //         return false;
        //     }
        // }
         private bool CheckDistance(double eventLatitute, double eventLongitude,double userLatitude,double userLongitude)
    {
        var d1 = eventLatitute * (Math.PI / 180.0);
        var num1 = eventLongitude * (Math.PI / 180.0);
        var d2 = userLatitude * (Math.PI / 180.0);
        var num2 = userLongitude * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                 Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
        var distance = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));

        if (distance < 5000){
            return true;
        }

        return false;
    }
    }
}
