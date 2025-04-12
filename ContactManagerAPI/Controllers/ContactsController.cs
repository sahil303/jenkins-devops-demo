using Microsoft.AspNetCore.Mvc;
using ContactManagerAPI.Data;
using ContactManagerAPI.Models;

namespace ContactManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactContext _context;

        public ContactsController(ContactContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> Get()
        {
            return Ok(_context.Contacts.ToList());
        }

        [HttpPost]
        public IActionResult Post(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
        }
    }
}
