using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using RoomBookingBackend.Models;

namespace RoomBookingBackend.DTOs
{
    // Request (Membuat data dari client)
    public class CreateBookingDto
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BookedBy { get; set; } = string.Empty;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [MaxLength(255)]
        public string Purpose { get; set; } = string.Empty;
    }

    // Response (Menampilkan data ke client)
    public class BookingDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public string BookedBy { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? StatusUpdatedAt { get; set; }
    }

    // Request (update status booking)
    public class UpdateBookingStatusDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}