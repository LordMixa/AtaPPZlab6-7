using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using AtaPPZlab6_7.Models;
using UI.Models;

namespace UI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ShowController : Controller
    {
        private readonly ProgramLogic _programLogic;
        private readonly IService _showservice;
        public ShowController(ProgramLogic programLogic)
        {
            _programLogic = programLogic;
            _showservice = programLogic.showService;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Show> shows = _programLogic.GetEShows();
            if (id > shows.Count)
                return NotFound();
            else
            {
                Show show = shows[--id];
                var showModel = new ShowModel
                {
                    Name = show.Name,
                    Author = show.Author,
                    Genre = show.Genre,
                    CountSeats = show.CountSeats,
                    Date = show.Date.ToString(),
                    Price = show.Price
                };
                return View(showModel);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] ShowModel value)
        {
            _programLogic.AddShow(value.Name, value.Author, value.Genre, value.CountSeats, DateTime.Parse(value.Date), value.Price);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ShowModel value)
        {
            List<Show> shows = _programLogic.GetEShows();
            if (id > shows.Count)
                return NotFound();
            Show show = new Show(value.Name, value.Author, value.Genre, value.CountSeats, DateTime.Parse(value.Date), value.Price);
            _programLogic.UpdateShow( show, id);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            List<Show> shows = (List<Show>)_showservice.GetEntity();
            if (id > shows.Count)
                return Json(new { message = "Show wasn`t found" });
            return Json(new { message = _programLogic.DeleteShow(id) });
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Show> shows = _programLogic.GetEShows();
            var showModels = new List<ShowModel>();
            foreach (var show in shows)
            {
                if (show is Show concreteTicket)
                {
                    var showModel = new ShowModel
                    {
                        Name = concreteTicket.Name,
                        Author = concreteTicket.Author,
                        Genre = concreteTicket.Genre,
                        CountSeats = concreteTicket.CountSeats,
                        Date = concreteTicket.Date.ToString(),
                        Price = concreteTicket.Price
                    };

                    showModels.Add(showModel);
                }
            }
            return View(showModels);
        }
    }
}
