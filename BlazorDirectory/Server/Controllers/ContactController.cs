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

        [HttpGet("listContact")]
        public List<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }

        [HttpGet("listContact/{name}")]
        public Contact GetContactListName(string name)
        {
            Contact contact = _context.Contacts.SingleOrDefault(e => e.name == name);
            if (contact == null)
            {
                return new Contact();
            }
            return contact;
        }

        [HttpPost("addContact")]
        public IActionResult AddContact(Contact contact)
        {
            if (GetContactSearchName(contact.name) == contact.name)
            {
                return NotFound("Contacto existe en el directorio");
            }
            else
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
                return Ok(_context.Contacts.ToList());
            }
        }

        [HttpGet("searchContact/{name}")]
        public string GetContactSearchName(string name)
        {
            string message = "";
            try
            {
                var _contact = _context.Contacts.SingleOrDefault(x => x.name == name);
                if (_contact == null)
                {
                    message = "Contacto con el nombre de " + name + " no se encontro en el directorio";
                    return message;
                }
                message = name;
                return message;
            }
            catch (System.Exception)
            {

                message = "Error ingrese un nombre, recurde que no se permiten espacios vacios o nulos";
                return message;
            }
        }

        [HttpGet("existsContact/{name}")]
        public string GetContactExistName(string name)
        {
            var _contact = _context.Contacts.FirstOrDefault(x => x.name == name);
            string message = "";
            if (_context.Contacts.ToList().Count() > 0)
            {
                if (_contact != null)
                {
                    message = "Contacto con el nombre "+ name+ " existe";
                    return message;
                }
            }
            message = "No existe " +name+ " en el directorio telefonico";
            return message;
        }

        [HttpDelete("deleteContact/{name}")]
        public IActionResult DeleteContact(string name)
        {
            var _contact = _context.Contacts.SingleOrDefault(x => x.name == name);
            if (_contact == null)
            {
                return NotFound("Contacto con el nombre " + name + " no existe en el directorio");
            }
            _context.Contacts.Remove(_contact);
            _context.SaveChanges();
            return Ok("Contacto con el nombre " + name + " ha sido eliminado");
        }

        // [HttpPost("deleteContact/{name}")]
        // public IActionResult DeleteContact(Contact contact, string name)
        // {
        //     var _contact = _context.Contacts.SingleOrDefault(x => x.name == contact.name);
        //     if (_contact == null)
        //     {
        //         return NotFound("Contacto con el nombre " + contact.name + " no existe en el directorio");
        //     }
        //     _context.Contacts.Remove(_contact);
        //     _context.SaveChanges();
        //     return Ok("Contacto con el nombre " + contact.name + " ha sido eliminado");
        // }


        [HttpPut("updateContact/{name}")]
        public IActionResult UpdateContact(Contact contact, string name)
        {
            var _contact = _context.Contacts.SingleOrDefault(x => x.name == name);
            if (_contact == null)
            {
                return NotFound("Contacto con el nombre " + name + " no existe");
            }
            if (contact.name != null)
            {
                _contact.name = contact.name;
            }
            if (contact.phoneNumber != null)
            {
                _contact.phoneNumber = contact.phoneNumber;
            }
            if (contact.cellPhone != null)
            {
                _contact.cellPhone = contact.cellPhone;
            }
            _context.Update(_contact);
            _context.SaveChanges();
            return Ok("Contacto con el nombre " + name + " actualido");
        }
    }
}