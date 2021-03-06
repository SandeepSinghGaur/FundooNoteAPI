﻿// <auto-generated />
using System;
using FundooNotesRepositoryLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FundooNotesRepositoryLayer.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20210313023403_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FundooNotesModelLayer.Collaborator", b =>
                {
                    b.Property<int>("CollaboratorId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NoteId");

                    b.Property<string>("ReceiverEmail");

                    b.Property<long>("UserId");

                    b.HasKey("CollaboratorId");

                    b.ToTable("Collaborators");
                });

            modelBuilder.Entity("FundooNotesModelLayer.Label", b =>
                {
                    b.Property<int>("LabelId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Labels");

                    b.Property<int>("NoteId");

                    b.Property<long>("UserId");

                    b.HasKey("LabelId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("FundooNotesModelLayer.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<bool>("IsArchive");

                    b.Property<bool>("IsPin");

                    b.Property<bool>("IsTrash");

                    b.Property<DateTime>("NoteCreatedDate");

                    b.Property<DateTime>("NoteModifiedDate");

                    b.Property<DateTime?>("Remainder");

                    b.Property<string>("Title");

                    b.Property<long>("UserId");

                    b.HasKey("NoteId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("FundooNotesModelLayer.UserRegistration", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Mobile")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
