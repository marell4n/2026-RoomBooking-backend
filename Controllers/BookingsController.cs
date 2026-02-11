using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBookingBackend.Data;
using RoomBookingBackend.Models;
using RoomBookingBackend.DTOs;

namespace RoomBookingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            // Maping entity ke Dto
            var bookings = await _context.Bookings
                .Include(b => b.Room)
                .Where(b => !b.IsDeleted) // Filter soft deleted bookings
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    RoomId = b.RoomId,
                    RoomName = b.Room != null ? b.Room.Name : "-",
                    BookedBy = b.BookedBy,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    Purpose = b.Purpose,
                    Status = b.Status.ToString(),
                    StatusUpdatedAt = b.StatusUpdatedAt,
                    UpdatedAt = b.UpdatedAt
                })
                .ToListAsync();

            return bookings;
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null || booking.IsDeleted)
            {
                return NotFound();
            }

            var bookingDto = new BookingDto
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                RoomName = booking.Room != null ? booking.Room.Name : "-",
                BookedBy = booking.BookedBy,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Purpose = booking.Purpose,
                Status = booking.Status.ToString(),
                StatusUpdatedAt = booking.StatusUpdatedAt,
                UpdatedAt = booking.UpdatedAt
            };

            return bookingDto;
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<BookingDto>> PostBooking(CreateBookingDto bookingDto)
        {
            // Validasi: Cek apakah Room ada (dan tidak dihapus)
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == bookingDto.RoomId && !r.IsDeleted);
            if (room == null)
            {
                return BadRequest("Invalid Room ID or Room is unavailable.");
            }

            // Validasi: Cek apakah jadwal bentrok (Opsional tapi penting)
            bool isConflict = await _context.Bookings.AnyAsync(b => 
                b.RoomId == bookingDto.RoomId &&
                !b.IsDeleted && // Abaikan booking yang sudah dihapus
                b.Status != Booking.BookingStatus.Rejected && // Abaikan yang ditolak
                b.Status != Booking.BookingStatus.Cancelled && // Abaikan yang batal
                ((bookingDto.StartTime < b.EndTime && bookingDto.StartTime >= b.StartTime) || 
                 (bookingDto.EndTime > b.StartTime && bookingDto.EndTime <= b.EndTime))
            );

            if (isConflict)
            {
                return Conflict("The room is already booked for the selected time.");
            }

            // Mapping DTO -> Entity
            var booking = new Booking
            {
                RoomId = bookingDto.RoomId,
                BookedBy = bookingDto.BookedBy,
                StartTime = bookingDto.StartTime,
                EndTime = bookingDto.EndTime,
                Purpose = bookingDto.Purpose,
                Status = Booking.BookingStatus.Pending, // Default status
                IsDeleted = false,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Load ulang data booking beserta Room-nya untuk response
            await _context.Entry(booking).Reference(b => b.Room).LoadAsync();

            var resultDto = new BookingDto
            {
                Id = booking.Id,
                RoomId = booking.RoomId,
                RoomName = booking.Room?.Name ?? "-",
                BookedBy = booking.BookedBy,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                Purpose = booking.Purpose,
                Status = booking.Status.ToString(),
                StatusUpdatedAt = booking.StatusUpdatedAt,
                UpdatedAt = booking.UpdatedAt
            };

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, resultDto);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null || booking.IsDeleted)
            {
                return NotFound();
            }

            // Soft delete
            booking.IsDeleted = true;
            booking.Status = Booking.BookingStatus.Cancelled; // Set status to Cancelled
            booking.StatusUpdatedAt = DateTime.UtcNow;
            booking.UpdatedAt = DateTime.UtcNow;

            _context.Entry(booking).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //PUT: api/Bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, CreateBookingDto bookingDto)
        {
            var booking = await _context.Bookings.FindAsync(id);

            // Cek apakah booking ada dan belum dihapus
            if (booking == null || booking.IsDeleted)
            {
                return NotFound();
            }

            // Validasi Konflik Jadwal
            // Cek apakah jadwal baru bentrok dengan booking lain (kecuali booking ini sendiri)
            bool isConflict = await _context.Bookings.AnyAsync(b => 
                b.RoomId == bookingDto.RoomId &&
                b.Id != id && // Abaikan diri sendiri
                !b.IsDeleted &&
                b.Status != Booking.BookingStatus.Rejected &&
                b.Status != Booking.BookingStatus.Cancelled &&
                ((bookingDto.StartTime < b.EndTime && bookingDto.StartTime >= b.StartTime) || 
                 (bookingDto.EndTime > b.StartTime && bookingDto.EndTime <= b.EndTime))
            );

            if (isConflict)
            {
                return Conflict("The room is already booked for the updated time.");
            }

            // Update Data
            booking.RoomId = bookingDto.RoomId;
            booking.BookedBy = bookingDto.BookedBy;
            booking.StartTime = bookingDto.StartTime;
            booking.EndTime = bookingDto.EndTime;
            booking.Purpose = bookingDto.Purpose;
            
            // Catat waktu update
            booking.UpdatedAt = DateTime.UtcNow;

            // Simpan ke Database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // PATCH: api/Bookings/5/status
        // Digunakan khusus untuk mengubah Status (Approve/Reject/Cancel)
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateBookingStatusDto([FromRoute] int id, [FromBody] UpdateBookingStatusDto statusDto)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null || booking.IsDeleted)
            {
                return NotFound();
            }

            // Update Status
            booking.Status = statusDto.Status;

            // Update Timestamp Audit (Otomatis dari Server)
            booking.StatusUpdatedAt = DateTime.UtcNow;
            
            // Update timestamp umum juga
            booking.UpdatedAt = DateTime.UtcNow;
            

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id && !e.IsDeleted);
        }
    }
}