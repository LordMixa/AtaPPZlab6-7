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
        private readonly IService _ticketservice;
        public TicketController(IService showService)
        {
            _ticketservice = showService;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Ticket> tickets = (List<Ticket>)_ticketservice.GetEntity();
            if (id > tickets.Count)
                return NotFound();
            else
            {
                Ticket ticket = tickets[--id];
                var ticketModel = new TicketViewModel
                {
                    Name = ticket.Name,
                    NameOfOwner = ticket.NameOfOwner,
                    Date = ticket.Date,
                    Price = ticket.Price
                };
                return View(ticketModel);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Ticket value)
        {
            _ticketservice.AddEntity(value);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Ticket value)
        {
            List<Ticket> tickets = (List<Ticket>)_ticketservice.GetEntity();
            if (id > tickets.Count)
                return NotFound();
            _ticketservice.UpdateEntity(tickets[--id].NameOfOwner, value);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            List<Ticket> tickets = (List<Ticket>)_ticketservice.GetEntity();
            if (id > tickets.Count)
                return NotFound();
            Ticket ticket = tickets[--id];
            _ticketservice.DeleteEntity(ticket.Name, ticket.NameOfOwner);
            return Ok();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var tickets = _ticketservice.GetEntity();
            var ticketModels = new List<TicketViewModel>();

            foreach (var ticket in tickets)
            {
                if (ticket is Ticket concreteTicket)
                {
                    var ticketModel = new TicketViewModel
                    {
                        Name = concreteTicket.Name,
                        NameOfOwner = concreteTicket.NameOfOwner,
                        Date = concreteTicket.Date,
                        Price = concreteTicket.Price
                    };

                    ticketModels.Add(ticketModel);
                }
            }
            return View(ticketModels);
        }
    }
}
