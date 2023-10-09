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

        [HttpGet]
        [Route("today")]
        public ActionResult TodaysBookings()
        {
            var bookings = _context.Bookings.Where(b => b.StartTime.Date == DateTime.UtcNow.Date);
            return new JsonResult(bookings);
        }
        [HttpGet]
        [Route("comingweek")]
        public ActionResult ComingWeeksBookings()
        {
            var validDates = new List<DateTime>
            {
                DateTime.UtcNow.Date.AddDays(1),
                DateTime.UtcNow.Date.AddDays(2),
                DateTime.UtcNow.Date.AddDays(3),
                DateTime.UtcNow.Date.AddDays(4),
                DateTime.UtcNow.Date.AddDays(5),
                DateTime.UtcNow.Date.AddDays(6),
                DateTime.UtcNow.Date.AddDays(7)
            };
            var bookings = _context.Bookings.Where(b => validDates.Contains(b.StartTime.Date));
            return new JsonResult(bookings);
        }
    }
}
