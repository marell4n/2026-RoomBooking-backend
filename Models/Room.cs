using System.ComponentModel.DataAnnotations;

namespace RoomBookingBackend.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        // relasi one-to-many dengan Booking
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}