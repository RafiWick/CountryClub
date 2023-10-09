using CountryClubAPI.DataAccess;
using CountryClubAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace CountryClubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly CountryClubContext _context;

        public BookingsController(CountryClubContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult CreateBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            Response.StatusCode = 201;
            return new JsonResult(booking);
        }
    }
}
