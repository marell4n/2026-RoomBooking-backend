using System.ComponentModel.DataAnnotations;

namespace RoomBookingBackend.DTOs
{
    // Request (Membuat/update data dari client)
    public class CreateRoomDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;
    }

    // Response (Menampilkan data ke client)
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}