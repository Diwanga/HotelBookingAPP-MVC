using System.Globalization;
using HotelBookingAPP.Models;

namespace HotelBookingAPP.Services
{
    public static class ChatbotService
    {
        public static string GetResponse(string input)
        {
            input = input.ToLower();

            if (input.Contains("get") && input.Contains("rooms in") && input.Contains("to"))
            {
                try
                {
                    var roomCountMatch = System.Text.RegularExpressions.Regex.Match(input, @"get (\d+) rooms");
                    if (!roomCountMatch.Success) return "Please specify number of rooms like 'get 2 rooms'";
                    int requestedRooms = int.Parse(roomCountMatch.Groups[1].Value);
                    
                    var dateRangeMatch = System.Text.RegularExpressions.Regex.Match(input, @"in (\d{4}/\d{2}/\d{2}) to (\d{4}/\d{2}/\d{2})");
                    if (!dateRangeMatch.Success) return "Please use date format: yyyy/MM/dd";

                    var startDate = DateTime.ParseExact(dateRangeMatch.Groups[1].Value, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    var endDate = DateTime.ParseExact(dateRangeMatch.Groups[2].Value, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                    int totalDays = (endDate - startDate).Days;
                    if (totalDays <= 0) return "Check-out date must be after check-in.";
                    
                    DateTime lastMonthStart = startDate.AddMonths(-1);
                    DateTime lastMonthEnd = endDate.AddMonths(-1);

                    var predictedAvailableRoomIds = new HashSet<int>();

                    for (int i = 0; i < totalDays; i++)
                    {
                        var targetDate = startDate.AddDays(i);
                        var matchingDate = lastMonthStart.AddDays(i);
                        var bookedRoomIds = BookingService.Bookings
                            .Where(b => b.CheckIn <= matchingDate && b.CheckOut > matchingDate)
                            .Select(b => b.RoomId)
                            .ToHashSet();

                        var availableRoomIds = BookingService.Rooms
                            .Select(r => r.Id)
                            .Where(id => !bookedRoomIds.Contains(id))
                            .ToList();
                        if (i == 0)
                            predictedAvailableRoomIds = new HashSet<int>(availableRoomIds);
                        else
                            predictedAvailableRoomIds.IntersectWith(availableRoomIds);
                    }
                    var availableRooms = BookingService.Rooms
                        .Where(r => predictedAvailableRoomIds.Contains(r.Id))
                        .ToList();

                    if (availableRooms.Count >= requestedRooms)
                    {
                        var avgPrice = availableRooms.Average(r => r.PricePerDay);
                        return $"Yes, {requestedRooms} rooms will likely be available. Estimated avg. price per day: Rs. {avgPrice:F2}";
                    }
                    else
                    {
                        return $"Probably not enough rooms available for those dates. Only {availableRooms.Count} rooms were free last month during that pattern.";
                    }
                }
                catch
                {
                    return "Could not understand. Use format: 'Will it be available to get 2 rooms in yyyy/MM/dd to yyyy/MM/dd'";
                }
            }
            return "Please ask like: 'Will it be available to get 2 rooms in yyyy/MM/dd to yyyy/MM/dd'";
        }
    }
}
