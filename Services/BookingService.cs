using HotelBookingAPP.Models;

namespace HotelBookingAPP.Services
{
    public static class BookingService
    {
        public static List<Room> Rooms { get; private set; } = new();
        public static List<Booking> Bookings { get; private set; } = new();

        static BookingService()
        {
            InitializeData();
        }

        public static void InitializeData()
        {
            // Create 10 rooms
            Rooms = new List<Room>();
            for (int i = 1; i <= 10; i++)
            {
                Rooms.Add(new Room
                {
                    Id = i,
                    RoomType = i % 2 == 0 ? "Deluxe " + i : "Standard " + i,
                    Capacity = i % 3 + 1,
                    IsAvailable = true,
                    PricePerDay = 100 + (i * 20)
                });
            }

            // Create 100 bookings from the last month
            var today = DateTime.Today;
            var startOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);

            var rnd = new Random();
            Bookings = new List<Booking>();

            for (int i = 1; i <= 100; i++)
            {
                var dayOffset = rnd.Next(0, 28);
                var checkIn = startOfLastMonth.AddDays(dayOffset);
                var checkOut = checkIn.AddDays(rnd.Next(1, 4));
                var roomId = rnd.Next(1, 11);

                var room = Rooms.First(r => r.Id == roomId);
                var nights = (checkOut - checkIn).Days;
                Bookings.Add(new Booking
                {
                    Id = i,
                    RoomId = roomId,
                    Room = Rooms.First(r => r.Id == roomId),
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    GuestName = $"Guest {i}",
                    TotalPrice = room.PricePerDay * nights,
                    SpecialRequests = new List<SpecialRequest>
                    {
                        new SpecialRequest { Id = i, RequestText = "Extra pillows" }
                    }
                });
            }
        }

        public static Dictionary<string, List<Booking>> GetWeeklyReport()
        {
            var report = new Dictionary<string, List<Booking>>();
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek); // Sunday

            for (int i = 0; i < 7; i++)
            {
                var date = startOfWeek.AddDays(i).Date;
                var key = date.ToString("dddd");

                var dailyBookings = Bookings
                    .Where(b => b.CheckIn <= date && b.CheckOut > date)
                    .ToList();

                report[key] = dailyBookings;
            }

            return report;
        }

        public static bool IsRoomAvailableOn(string roomType, DateTime date)
        {
            var roomIdsOfType = Rooms.Where(r => r.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase)).Select(r => r.Id).ToList();

            var roomsBooked = Bookings
                .Where(b => roomIdsOfType.Contains(b.RoomId) &&
                            b.CheckIn <= date &&
                            b.CheckOut > date)
                .Select(b => b.RoomId)
                .ToList();

            return roomIdsOfType.Any(id => !roomsBooked.Contains(id));
        }
    }
}
