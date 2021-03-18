using FundooNotesModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesRepositoryLayer.Repository
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
           : base(options)
        {
        }
        public DbSet<UserRegistration> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
    }
}
