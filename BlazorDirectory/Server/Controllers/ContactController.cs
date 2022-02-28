using Microsoft.AspNetCore.Mvc;
using BlazorDirectory.Shared;


namespace BlazorTelephoneDirectory.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactContext _context;
        private int size = 10;
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
                message = "Contacto con el nombre de " + name + " se encuentra en el directorio";
                return message;
            }
            catch (System.Exception)
            {

                message = "Error ingrese un nombre, recurde que no se permiten espacios vacios o nulos";
                return message;
            }
        }

        [HttpGet("existContact/{name}")]
        public string GetContactExistName(string name)
        {
            var _contact = _context.Contacts.FirstOrDefault(x => x.name == name);
            string message = "";
            if (_context.Contacts.ToList().Count() > 0)
            {
                if (_contact != null)
                {
                    message = "El contacto si existe en el directorio";
                    return message;
                }
            }
            message = "No existe este contacto en el directorio";
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

        [HttpGet("spaceContact")]
        public string DirectoryWithSpace()
        {
            int directoryWithSpace = 0;
            string message = "";
            directoryWithSpace = size - _context.Contacts.ToList().Count();
            if (directoryWithSpace > 0)
            {
                message = "Hay " + directoryWithSpace + " espacios para guardar contactos ";
                return message;
            }
            else
            {
                message = "No quedan espacios en el directorio para guardar contactos";
                return message;
            }
        }

        [HttpGet("directoryFull")]
        public string DirectoryFull()
        {
            int directoryCount = size - _context.Contacts.ToList().Count();
            string message = "";
            if (directoryCount > 0)
            {
                message = "La agenda aún conserva espacio para agregar contactos.";
                return message;
            }
            else
            {
                message = "No hay espacio en la agenda.";
                return message;
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