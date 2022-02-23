using Microsoft.AspNetCore.Mvc;
using BlazorDirectory.Shared;


namespace BlazorTelephoneDirectory.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext _context;
        private int size = 3;
        public ContactController(ContactContext context)
        {
            _context = context;
        }

        [HttpGet("listContact")]
        public List<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }

        [HttpPost("addContact")]
        public IActionResult AddContact(Contact contact)
        {
            int directoryCount = size - _context.Contacts.ToList().Count();
            if (directoryCount > 0)
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
                return Ok(_context.Contacts.ToList());
            }
            else
            {
                return NotFound("No hay espacio en la agenda");
            }
        }

        [HttpGet("searchContact/{name}")]
        public IActionResult GetContactSearchName(string name)
        {
            var _contact = _context.Contacts.SingleOrDefault(x => x.name == name);

            if (_contact == null)
            {
                return NotFound("Contacto con el nombre de " + name + " no se encontro en el directorio");
            }

            return Ok("Contacto con el nombre de " + name + " se encuentra en el directorio");
        }

        [HttpGet("existContact/{name}")]
        public IActionResult GetContactExistName(string name)
        {
            var _contact = _context.Contacts.SingleOrDefault(x => x.name == name);

            if (_context.Contacts.ToList().Count() > 0)
            {
                if (_contact == null)
                {
                    return NotFound("No existe este contacto en el directorio");
                }
            }
            return Ok("El contacto si existe en el directorio");
        }

        [HttpDelete("deleteContact/{name}")]
        public IActionResult DeleteContact(string name)
        {
            var cont = _context.Contacts.SingleOrDefault(x => x.name == name);
            if (cont == null)
            {
                return NotFound("Contacto con el nombre " + name + " no existe en el directorio");
            }
            _context.Contacts.Remove(cont);
            _context.SaveChanges();
            return Ok("Contacto con el nombre " + name + " ha sido eliminado");
        }

        [HttpGet("spaceContact")]
        public IActionResult DirectoryWithSpace()
        {
            int directoryWithSpace = 0;
            directoryWithSpace = size - _context.Contacts.ToList().Count();
            if (directoryWithSpace > 0)
            {
                return Ok("Hay " + directoryWithSpace + " espacios para guardar contactos ");
            }
            else
            {
                return NotFound("No quedan espacios en el directorio para guardar contactos");
            }
        }

        [HttpGet("directoryFull")]
        public IActionResult DirectoryFull()
        {
            int directoryCount = size - _context.Contacts.ToList().Count();
            if (directoryCount > 0)
            {
                return Ok("La agenda aún conserva espacio para agregar contactos.");
            }else{
            return NotFound("No hay espacio en la agenda.");
            }
        }

        // [HttpPut("{id}")]
        // public IActionResult UpdateContact(int id, Contact contact)
        // {
        //     var cont = _context.Contacts.SingleOrDefault(x => x.ContactId == id);
        //     if (cont == null)
        //     {
        //         return NotFound("Contacto con el Id " + id + " no existe");
        //     }
        //     if (contact.name != null)
        //     {
        //         cont.name = contact.name;
        //     }
        //     if (contact.phoneNumber != null)
        //     {
        //         cont.phoneNumber = contact.phoneNumber;
        //     }
        //     if (contact.cellPhone != null)
        //     {
        //         cont.cellPhone = contact.cellPhone;
        //     }

        //     _context.Update(cont);
        //     _context.SaveChanges();
        //     return Ok("Contacto con el Id " + id + " actualido");
        // }
    }
}