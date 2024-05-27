using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ITHelpDesk.Web.Models;

namespace ITHelpDesk.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITHelpDeskContext _context;

        public TicketController(ITHelpDeskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetTickets()
        {
            return _context.Tickets.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        [HttpPost]
        public ActionResult<Ticket> CreateTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketId }, ticket);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTicket(int id, Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/assign/{userId}")]
        public IActionResult AssignTicket(int id, int userId)
        {
            var ticket = _context.Tickets.Find(id);
            var user = _context.Users.Find(userId);

            if (ticket == null || user == null)
            {
                return NotFound();
            }

            ticket.AssignedTo = user;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/status/{status}")]
        public IActionResult UpdateTicketStatus(int id, TicketStatus status)
        {
            var ticket = _context.Tickets.Find(id);

            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Status = status;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
