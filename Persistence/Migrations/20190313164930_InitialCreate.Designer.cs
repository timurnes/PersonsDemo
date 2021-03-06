﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(PersonsDemoContext))]
    [Migration("20190313164930_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.Person", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<int>("Age")
                        .HasColumnName("Age")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Model.Person", b =>
                {
                    b.OwnsOne("Model.PersonalName", "PersonalName", b1 =>
                        {
                            b1.Property<Guid>("PersonId");

                            b1.HasKey("PersonId");

                            b1.ToTable("Persons");

                            b1.HasOne("Model.Person")
                                .WithOne("PersonalName")
                                .HasForeignKey("Model.PersonalName", "PersonId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("Model.Name", "FirstName", b2 =>
                                {
                                    b2.Property<Guid>("PersonalNamePersonId");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnName("FirstName")
                                        .HasColumnType("nvarchar(100)");

                                    b2.HasKey("PersonalNamePersonId");

                                    b2.ToTable("Persons");

                                    b2.HasOne("Model.PersonalName")
                                        .WithOne("FirstName")
                                        .HasForeignKey("Model.Name", "PersonalNamePersonId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("Model.Name", "LastName", b2 =>
                                {
                                    b2.Property<Guid>("PersonalNamePersonId");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnName("LastName")
                                        .HasColumnType("nvarchar(100)");

                                    b2.HasKey("PersonalNamePersonId");

                                    b2.ToTable("Persons");

                                    b2.HasOne("Model.PersonalName")
                                        .WithOne("LastName")
                                        .HasForeignKey("Model.Name", "PersonalNamePersonId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
