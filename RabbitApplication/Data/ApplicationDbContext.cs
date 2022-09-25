using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RabbitApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LoginKrunal> ToDoItem { get; set; }
        public DbSet<LoginRachita> Rachita { get; set; }
    }
}
