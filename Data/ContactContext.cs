using Microsoft.EntityFrameworkCore;
using BlazorTelephoneDirectory.Models;

namespace BlazorTelephoneDirectory.Data{

    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
        :base(options)
        {}

        public DbSet<Contact> Contacts { get; set;}
    }
}