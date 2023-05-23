using Microsoft.AspNetCore.Mvc;
using BLL;
using UI.Models;

namespace UI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly ProgramLogic _programLogic;
        private readonly IService _ticketservice;
        public TicketController(ProgramLogic programLogic)
        {
            _programLogic = programLogic;
            _ticketservice = programLogic.showService;
        }
        [HttpGet("{id}")]
        public ActionResult<Ticket> Get(int id)
        {
            List<Ticket> tickets = _programLogic.GetETickets();
            if (id > tickets.Count)
                return NotFound();
            else
                return tickets[--id];
        }
        [HttpPost]
        public ActionResult Post(string nameowner,int id)
        {
            return Json(new { message = _programLogic.AddTicket(id, nameowner) });
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, int idshow,string nameown)
        {
            List<Ticket> tickets = _programLogic.GetETickets();
            if (id > tickets.Count)
                return NotFound();
            _programLogic.UpdateTicket(id,idshow, nameown);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id,string nameown)
        {
            return Json(new { message = _programLogic.DeleteTicket(id, nameown) });
        }
        [HttpGet]
        public IActionResult Index()
        {
            var tickets = _programLogic.GetETickets();
            var ticketModels = new List<TicketModel>();

            foreach (var ticket in tickets)
            {
                if (ticket is Ticket concreteTicket)
                {
                    var ticketModel = new TicketModel
                    {
                        Name = concreteTicket.Name,
                        NameOfOwner = concreteTicket.NameOfOwner,
                        Date = concreteTicket.Date.ToString(),
                        Price = concreteTicket.Price
                    };

                    ticketModels.Add(ticketModel);
                }
            }
            return View(ticketModels);
        }
    }
}