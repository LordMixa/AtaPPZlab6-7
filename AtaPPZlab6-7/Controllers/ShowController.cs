using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Newtonsoft.Json.Linq;

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
        [HttpGet]
        public IEnumerable<Show> Get()
        {
            return _showservice.GetShows();
        }
        [HttpGet("{id}")]
        public ActionResult<Show> Get(int id)
        {
            List<Show> shows = (List<Show>)_showservice.GetShows();
            if (id >= shows.Count)
                return NotFound();
            else
                return shows[--id];
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
            if (id >= shows.Count)
                return NotFound();
            Show show = shows[--id];
            _showservice.UpdateShow(show.Name, show.Author, value);
            return Ok();
        }

        // DELETE api/sample/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            List<Show> shows = (List<Show>)_showservice.GetShows();
            if (id >= shows.Count)
                return NotFound();
            Show show = shows[--id];
            _showservice.DeleteShow(show.Name, show.Author);
            return Ok();
        }
    }
}
