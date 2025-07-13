using Microsoft.AspNetCore.Mvc;
using HotelBookingAPP.Models;
using HotelBookingAPP.Services;

namespace HotelBookingAPP.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index() => View(BookingService.Bookings);

        public IActionResult Create()
        {
            ViewBag.Rooms = BookingService.Rooms;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Booking booking,string specialRequestText)
        {
            // Check room availability for selected date range
            var overlapping = BookingService.Bookings.Any(b =>
                b.RoomId == booking.RoomId &&
                booking.CheckIn < b.CheckOut && booking.CheckOut > b.CheckIn
            );
            if (overlapping)
            {
                ModelState.AddModelError("", "Selected room is not available between the selected dates.");
                ViewBag.Rooms = BookingService.Rooms;
                return View(booking);
            }
            
            booking.Id = BookingService.Bookings.Max(b => b.Id) + 1;
            booking.Room = BookingService.Rooms.FirstOrDefault(r => r.Id == booking.RoomId);
            int nights = (booking.CheckOut - booking.CheckIn).Days;
            booking.TotalPrice = booking.Room.PricePerDay * nights;

            if (!string.IsNullOrEmpty(specialRequestText))
            {
                booking.SpecialRequests.Add(new SpecialRequest { Id = 1, RequestText = specialRequestText });
            }
            BookingService.Bookings.Add(booking);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var booking = BookingService.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        public IActionResult Edit(int id,string specialRequestText)
        {
            var booking = BookingService.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();
            ViewBag.Rooms = BookingService.Rooms;
            
            return View(booking);
        }

        [HttpPost]
        public IActionResult Edit(Booking updatedBooking, string specialRequestText)
        {
            var booking = BookingService.Bookings.FirstOrDefault(b => b.Id == updatedBooking.Id);
            if (booking == null) return NotFound();
            
            // Check room availability for selected date range
            var overlapping = BookingService.Bookings.Any(b =>
                b.RoomId == booking.RoomId &&
                booking.CheckIn < b.CheckOut && booking.CheckOut > b.CheckIn
            );

            if (overlapping)
            {
                ModelState.AddModelError("", "Selected room is not available between the selected dates.");
                ViewBag.Rooms = BookingService.Rooms;
                return View(booking);
            }

            booking.RoomId = updatedBooking.RoomId;
            booking.Room = BookingService.Rooms.FirstOrDefault(r => r.Id == updatedBooking.RoomId);
            int nights = (booking.CheckOut - booking.CheckIn).Days;
            booking.TotalPrice = booking.Room.PricePerDay * nights;
            booking.CheckIn = updatedBooking.CheckIn;
            booking.CheckOut = updatedBooking.CheckOut;
            booking.GuestName = updatedBooking.GuestName;

            if (!string.IsNullOrEmpty(specialRequestText))
            {
                booking.SpecialRequests.Clear();
                booking.SpecialRequests.Add(new SpecialRequest { Id = 1, RequestText = specialRequestText });
            }
            return RedirectToAction("Index");
        }

        public IActionResult WeeklyReport()
        {
            var report = BookingService.GetWeeklyReport();
            return View(report);
        }
        
        public IActionResult Delete(int id)
        {
            var booking = BookingService.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = BookingService.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking != null)
                BookingService.Bookings.Remove(booking);

            return RedirectToAction("Index");
        }
    }
}
