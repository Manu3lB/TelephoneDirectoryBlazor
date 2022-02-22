using Microsoft.AspNetCore.Mvc;
using BlazorDirectory.Shared;


namespace BlazorTelephoneDirectory.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext _context;

        public ContactController(ContactContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }
        // [HttpGet]
        // public Contact GetContactById(int id)
        // {
        //     return _context.Contacts.SingleOrDefault(e => e.ContactId == id);
        // }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var cont = _context.Contacts.SingleOrDefault(x => x.ContactId == id);
            if (cont == null)
            {
                return NotFound("Contacto con el Id " + id + " no existe");
            }
            _context.Contacts.Remove(cont);
            _context.SaveChanges();
            return Ok("Contacto con el Id " + id + " eliminado");
        }

        [HttpPost]
        public IActionResult AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return Ok(_context.Contacts.ToList());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, Contact contact)
        {
            var cont = _context.Contacts.SingleOrDefault(x => x.ContactId == id);
            if (cont == null)
            {
                return NotFound("Contacto con el Id " + id + " no existe");
            }
            if(contact.name != null)
            {
                cont.name=contact.name;
            }
            if(contact.phoneNumber != null)
            {
                cont.phoneNumber=contact.phoneNumber;
            }
            if(contact.cellPhone != null)
            {
                cont.cellPhone=contact.cellPhone;
            }

            _context.Update(cont);
            _context.SaveChanges();
            return Ok("Contacto con el Id " +id+ " actualido");
        }
    }
}