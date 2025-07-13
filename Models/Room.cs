using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPP.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string RoomType { get; set; }

        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;
        
        [Required]
        [Range(0, 999999)]
        public decimal PricePerDay { get; set; }
    }
}