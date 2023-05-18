using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Newtonsoft.Json.Linq;
using AtaPPZlab6_7.Models;

namespace AtaPPZlab6_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ShowService _ticketservice;
        public TicketController(ShowService showService)
        {
            _ticketservice = showService;
        }
        [HttpGet("{id}")]
        public ActionResult<Ticket> Get(int id)
        {
            List<Ticket> tickets = (List<Ticket>)_ticketservice.GetTickets();
            if (id > tickets.Count)
                return NotFound();
            else
                return tickets[--id];
        }
        [HttpPost]
        public ActionResult Post([FromBody] Ticket value)
        {
            _ticketservice.AddTicket(value);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Ticket value)
        {
            List<Ticket> tickets = (List<Ticket>)_ticketservice.GetShows();
            if (id > tickets.Count)
                return NotFound();
            _ticketservice.UpdateTicket(tickets[--id].NameOfOwner, value);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            List<Ticket> tickets = (List<Ticket>)_ticketservice.GetTickets();
            if (id > tickets.Count)
                return NotFound();
            Ticket ticket = tickets[--id];
            _ticketservice.DeleteTicket(ticket.NameShow, ticket.NameOfOwner);
            return Ok();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var tickets = _ticketservice.GetTickets();
            var ticketmodel = tickets.Select(p => new TicketViewModel
            {
                NameShow = p.NameShow,
                NameOfOwner = p.NameOfOwner,
                Date = p.Date,
                Price = p.Price
            });
            return View(ticketmodel);
        }
    }
}
