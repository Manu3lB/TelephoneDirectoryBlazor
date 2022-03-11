using Microsoft.AspNetCore.Mvc;
using BlazorDirectory.Shared;

namespace BlazorTelephoneDirectory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext _context;
        BusinessLogicDirectory business;
        public ContactController(ContactContext context)
        {
            _context = context;
            business = new BusinessLogicDirectory(context);
        }

        [HttpGet("listContact")]
        public List<Contact> GetContacts()
        {
            return business.GetContacts();
        }

        //     [HttpGet("listContact/{name}")]
        //     public Contact GetContactListName(string name)
        //     {
        //         Contact contact = _context.Contacts.SingleOrDefault(e => e.name == name);
        //         if (contact == null)
        //         {
        //             return new Contact();
        //         }
        //         return contact;
        //     }

        [HttpPost("addContact")]
        public IActionResult AddContact(Contact contact)
        {
            business.AddContact(contact);
            return Ok(business.GetContacts());
        }

        [HttpGet("searchContact/{name}")]
        public Contact GetContactSearchName(string name)
        {
            return business.GetContactSearchName(name);
        }

        [HttpGet("existsContact/{name}")]
        public bool GetContactExistName(string name)
        {
            return business.GetContactExistName(name);
        }

        [HttpDelete("deleteContact/{name}")]
        public string DeleteContact(string name)
        {
            return business.DeleteContact(name);
        }

        //     [HttpPut("updateContact/{name}")]
        //     public IActionResult UpdateContact(Contact contact, string name)
        //     {
        //         var _contact = _context.Contacts.SingleOrDefault(x => x.name == name);
        //         if (_contact == null)
        //         {
        //             return NotFound("Contacto con el nombre " + name + " no existe");
        //         }
        //         if (contact.name != null)
        //         {
        //             _contact.name = contact.name;
        //         }
        //         if (contact.phoneNumber != null)
        //         {
        //             _contact.phoneNumber = contact.phoneNumber;
        //         }
        //         if (contact.cellPhone != null)
        //         {
        //             _contact.cellPhone = contact.cellPhone;
        //         }
        //         _context.Update(_contact);
        //         _context.SaveChanges();
        //         return Ok("Contacto con el nombre " + name + " actualido");
        //     }
    }
}
