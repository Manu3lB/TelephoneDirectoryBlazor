using BlazorDirectory.Shared;

namespace BlazorTelephoneDirectory
{
    public class BusinessLogicDirectory
    {
        private readonly ContactContext _context;
        public BusinessLogicDirectory(ContactContext context)
        {
            _context = context;
        }

        public List<Contact> GetContacts()
        {
            return _context.Contacts.ToList();
        }

        public Contact GetContactListName(string name)
        {
            Contact contact = _context.Contacts.SingleOrDefault(e => e.name == name);
            if (contact == null)
            {
                return new Contact();
            }
            return contact;
        }

        public void AddContact(Contact contact)
        {
            string name = contact.name;
            if (GetContactExistName(name))
            {
                Console.WriteLine("==      El contacto ya se encuentra en la lista   ==");
            }
            else
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();
                Console.WriteLine("==        Se agrego correctamente el contacto     ==");
            }
        }

        public Contact GetContactSearchName(string name)
        {
            string message = "";
            try
            {
                Contact _contact = _context.Contacts.SingleOrDefault(x => x.name == name);
                if (_contact == null)
                {
                    return new Contact();
                }
                return _contact;
            }
            catch (System.Exception)
            {

                message = "Error ingrese un nombre, recurde que no se permiten espacios vacios o nulos";
                Console.WriteLine(message);
                return new Contact();
            }
        }

        public Boolean GetContactExistName(string name)
        {
            var _contact = _context.Contacts.FirstOrDefault(f => f.name == name);
            string message = "";
            bool existsContact = false;

            if (_contact == null)
            {
                message = "No existe " + name + " en el directorio telefonico";
                return existsContact;
            }
            else
            {
                message = "Contacto con el nombre " + name + " existe";
                existsContact = true;
                return existsContact;
            }
        }

        public string DeleteContact(string name)
        {
            string message = "";
            var _contact = _context.Contacts.SingleOrDefault(x => x.name == name);
            if (_contact == null)
            {
                message = "Contacto con el nombre " + name + " no existe en el directorio";
                return message;
            }
            _context.Contacts.Remove(_contact);
            _context.SaveChanges();
            message = "Contacto con el nombre " + name + " ha sido eliminado";
            return message;
        }
    }
}