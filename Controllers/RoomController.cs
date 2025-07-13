using Microsoft.AspNetCore.Mvc;
using HotelBookingAPP.Models;
using HotelBookingAPP.Services;

namespace HotelBookingAPP.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index() => View(BookingService.Rooms);

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Room room)
        {
                room.Id = BookingService.Rooms.Max(r => r.Id) + 1;
                BookingService.Rooms.Add(room);
                return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var room = BookingService.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();
            return View(room);
        }

        [HttpPost]
        public IActionResult Edit(Room updatedRoom)
        {
            var room = BookingService.Rooms.FirstOrDefault(r => r.Id == updatedRoom.Id);
            if (room == null) return NotFound();

            room.RoomType = updatedRoom.RoomType;
            room.Capacity = updatedRoom.Capacity;
            room.IsAvailable = updatedRoom.IsAvailable;
            room.PricePerDay = updatedRoom.PricePerDay;

            return RedirectToAction("Index");
        }
    }
}