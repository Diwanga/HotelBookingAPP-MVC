using Microsoft.AspNetCore.Mvc;
using HotelBookingAPP.Services;

namespace HotelBookingAPP.Controllers
{
    public class ChatbotController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(string userMessage)
        {
            var reply = ChatbotService.GetResponse(userMessage);
            ViewBag.Response = reply;
            ViewBag.UserMessage = userMessage;
            return View();
        }
    }
}