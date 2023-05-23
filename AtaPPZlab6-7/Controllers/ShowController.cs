using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using AtaPPZlab6_7.Models;

namespace AtaPPZlab6_7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController:Controller
    {
        private readonly IService _showservice;
         public ShowController(IService showService) 
        { 
           _showservice= showService;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Show> shows = (List<Show>)_showservice.GetEntity();
            if (id > shows.Count)
                return NotFound();
            else
            {
                Show show = shows[--id];
                var showModel = new ShowViewModel
                {
                    Name = show.Name,
                    Author = show.Author,
                    Genre = show.Genre,
                    CountSeats = show.CountSeats,
                    Date = show.Date,
                    Price = show.Price
                };
                return View(showModel);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Show value)
        {
            _showservice.AddEntity(value);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Show value)
        {
            List<Show> shows = (List<Show>)_showservice.GetEntity();
            if (id > shows.Count)
                return NotFound();
            _showservice.UpdateEntity(shows[--id].Name, value);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            List<Show> shows = (List<Show>)_showservice.GetEntity();
            if (id > shows.Count)
                return NotFound();
            Show show = shows[--id];
            _showservice.DeleteEntity(show.Name, show.Author);
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var shows = _showservice.GetEntity();
            var showModels = new List<ShowViewModel>();
            foreach (var show in shows)
            {
                if (show is Show concreteTicket)
                {
                    var showModel = new ShowViewModel
                    {
                        Name = concreteTicket.Name,
                        Author = concreteTicket.Author,
                        Genre = concreteTicket.Genre,
                        CountSeats = concreteTicket.CountSeats,
                        Date = concreteTicket.Date,
                        Price = concreteTicket.Price
                    };

                    showModels.Add(showModel);
                }
            }
            return View(showModels);
        }
    }
}
