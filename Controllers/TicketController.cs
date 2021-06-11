using Back_EndTest.Data;
using Back_EndTest.Filter;
using Back_EndTest.Helpers;
using Back_EndTest.Models;
using Back_EndTest.Services;
using Back_EndTest.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Back_EndTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TestDbContext _context;
        private readonly IUriService _uriService;

        public TicketController(TestDbContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Tickets
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync();
            var totalRecords = await _context.Tickets.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Ticket>(pagedData, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Tickets.Where(a => a.id_ticket == id).FirstOrDefaultAsync();
            return Ok(new Response<Ticket>(customer));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, Ticket ticket)
        {
            if (id != ticket.id_ticket)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Incoherencia en la información suministrada" });
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Estado no encontrado" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> PostEstado(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = ticket.id_ticket }, ticket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Tickets.FindAsync(id);
            if (estado == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Estado no encontrado" });
            }

            _context.Tickets.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.id_ticket == id);
        }
    }
}
