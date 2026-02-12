using Microsoft.EntityFrameworkCore;
using RoomBookingBackend.Models;

namespace RoomBookingBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; } // Uncomment this line  for second migration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Hall D4", Capacity = 120, Description = "Ruangan luas di lantai 1 gedung D4. Fasilitas: 6 Kipas Angin, layar proyektor." },
                new Room { Id = 2, Name = "B.201", Capacity = 40, Description = "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 3, Name = "B.202", Capacity = 60, Description = "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 4, Name = "B.203", Capacity = 40, Description = "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 5, Name = "B.204", Capacity = 120, Description = "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 6, Name = "B.205", Capacity = 40, Description = "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 7, Name = "A.301", Capacity = 120, Description = "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 8, Name = "A.302", Capacity = 60, Description = "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 9, Name = "A.303", Capacity = 120, Description = "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 10, Name = "A.304", Capacity = 40, Description = "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 11, Name = "B.301", Capacity = 60, Description = "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 12, Name = "B.302", Capacity = 40, Description = "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 13, Name = "B.303", Capacity = 120, Description = "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 14, Name = "B.304", Capacity = 60, Description = "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." },
                new Room { Id = 15, Name = "B.305", Capacity = 40, Description = "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor." }
            );
        }
    }
}