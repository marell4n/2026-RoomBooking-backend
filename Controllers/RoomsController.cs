using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBookingBackend.Data;
using RoomBookingBackend.Models;
using RoomBookingBackend.DTOs;

namespace RoomBookingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms()
        {
            // Maping entity ke Dto
            var rooms = await _context.Rooms
                .Select(r => new RoomDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    Description = r.Description
                })
                .ToListAsync();

            return rooms;
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null || room.IsDeleted)
            {
                return NotFound();
            }

            // Maping entity ke Dto
            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                Description = room.Description
            };
        }

        // POST: api/Rooms
        [HttpPost]
        public async Task<ActionResult<RoomDto>> PostRoom(CreateRoomDto roomDto)
        {
            // Maping Dto ke entity
            var room = new Room
            {
                Name = roomDto.Name,
                Capacity = roomDto.Capacity,
                Description = roomDto.Description
            };

            // Simpan ke database
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            // Mapping Entity ke DTO (response ke user)
            var resultDTO = new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                Description = room.Description
            };

            return CreatedAtAction("GetRoom", new { id = room.Id }, resultDTO);
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, CreateRoomDto roomDto)
        {
            // Map DTO to entity
            // Cari data lama di database
            var existingRoom = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return BadRequest();
            }

            // update field lama dengan yang baru
            existingRoom.Name = roomDto.Name;
            existingRoom.Capacity = roomDto.Capacity;
            existingRoom.Description = roomDto.Description;

            _context.Entry(existingRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            // Soft delete
            room.IsDeleted = true; // Tandai terhapus
            _context.Entry(room).State = EntityState.Modified; // Update status di database
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id && !e.IsDeleted);
        }
    }
}