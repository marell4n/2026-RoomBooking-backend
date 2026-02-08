using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoomBookingBackend.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; } // Foreign Key ke Room

        // Navigation Property: Agar kita bisa akses detail ruangan dari booking ini
        [ForeignKey("RoomId")]
        public Room? Room { get; set; }

        [Required]
        [MaxLength(100)]
        public string BookedBy { get; set; } = string.Empty; // Nama Peminjam (Mahasiswa/Dosen)

        [Required]
        public DateTime StartTime { get; set; } // Waktu Mulai

        [Required]
        public DateTime EndTime { get; set; } // Waktu Selesai

        [MaxLength(255)]
        public string Purpose { get; set; } = string.Empty; // Keperluan peminjaman
    }
}