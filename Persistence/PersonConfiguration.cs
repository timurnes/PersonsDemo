using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model;

namespace Persistence {
    internal class PersonConfiguration : IEntityTypeConfiguration<Person> {

        public void Configure(EntityTypeBuilder<Person> builder) {
            builder.ToTable("Persons");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();
            
            var ageConverter = new ValueConverter<Age, Int32>(
                v => v.Value,
                v => new Age(v));
                
            builder
                .Property(p => p.Age)
                .HasConversion(ageConverter)
                .HasColumnName("Age")
                .HasColumnType("int")
                .IsRequired();
                
            /*
            builder.OwnsOne(p => p.Age, a => {
                a.Property(u => u.Value).HasColumnName("Age");
                a.Property(u => u.Value).HasColumnType("int");
                a.Property(u => u.Value).IsRequired();
            });
            */

            builder.OwnsOne(b => b.PersonalName, pn => {
                pn.OwnsOne(p => p.FirstName, fn => {
                    fn.Property(x => x.Value).HasColumnName("FirstName");
                    fn.Property(x => x.Value).HasColumnType("nvarchar(100)");
                    fn.Property(x => x.Value).IsRequired();
                });

                pn.OwnsOne(p => p.LastName, ln => {
                    ln.Property(x => x.Value).HasColumnName("LastName");
                    ln.Property(x => x.Value).HasColumnType("nvarchar(100)");
                    ln.Property(x => x.Value).IsRequired();
                });
            });

        }

    }
}
