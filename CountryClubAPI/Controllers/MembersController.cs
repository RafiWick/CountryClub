using CountryClubAPI.DataAccess;
using CountryClubAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountryClubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly CountryClubContext _context;

        public MembersController(CountryClubContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult AllMembers()
        {
            var members = _context.Members;

            return new JsonResult(members);
        }
        [HttpGet("{memberId}")]
        public ActionResult SingleMember(int memberId)
        {
            var member = _context.Members.FirstOrDefault(m => m.Id == memberId);
            return new JsonResult(member);
        }
        [HttpPost]
        public ActionResult AddMember(Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Members.Add(member);
            _context.SaveChanges();
            Response.StatusCode = 201;
            return new JsonResult(member);
        }
        [HttpPost("{memberId}")]
        public ActionResult UpdateMember(int memberId, Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (_context.Members.FirstOrDefault() == null)
            {
                return NotFound();
            }
            member.Id = memberId;
            _context.Members.Update(member);
            _context.SaveChanges();
            Response.StatusCode = 201;
            return new JsonResult(member);
        }
        [HttpDelete("{memberId}")]
        public ActionResult DeleteMember(int memberId)
        {
            var member = _context.Members.FirstOrDefault();
            if (member == null)
            {
                return NotFound();
            }
            _context.Members.Remove(member);
            _context.SaveChanges();
            return new JsonResult(_context.Members);
        }
    }
}
