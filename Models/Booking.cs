using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPP.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        public Room Room { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        public string GuestName { get; set; }
        
        public decimal TotalPrice { get; set; }

        public List<SpecialRequest> SpecialRequests { get; set; } = new();
    }
}