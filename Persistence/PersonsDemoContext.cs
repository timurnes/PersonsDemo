using Microsoft.EntityFrameworkCore;
using Model;

namespace Persistence {
    public class PersonsDemoContext : DbContext {

        public DbSet<Person> Persons { get; set; }

        public PersonsDemoContext(DbContextOptions<PersonsDemoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
