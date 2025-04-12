using Microsoft.EntityFrameworkCore;
using ContactManagerAPI.Models;
using System.Collections.Generic;

namespace ContactManagerAPI.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }
    }
}
