using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using BLL;

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

    }
}
