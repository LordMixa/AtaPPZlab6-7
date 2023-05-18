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
        private readonly ShowService _showservice;
        public ShowController(ShowService showService) 
        { 
           _showservice= showService;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Show> shows = (List<Show>)_showservice.GetShows();
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
            _showservice.AddShow(value);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Show value)
        {
            List<Show> shows = (List<Show>)_showservice.GetShows();
            if (id > shows.Count)
                return NotFound();
            _showservice.UpdateShow(shows[--id].Name, value);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            List<Show> shows = (List<Show>)_showservice.GetShows();
            if (id > shows.Count)
                return NotFound();
            Show show = shows[--id];
            _showservice.DeleteShow(show.Name, show.Author);
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            var shows = _showservice.GetShows();
            var showmodel = shows.Select(p => new ShowViewModel
            {
                Name = p.Name,
                Author= p.Author,
                Genre= p.Genre,
                CountSeats= p.CountSeats,
                Date= p.Date,
                Price= p.Price
            });
            return View(showmodel);
        }
    }
}
