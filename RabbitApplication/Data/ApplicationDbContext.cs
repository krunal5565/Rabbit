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

        public DbSet<RoleType> RoleType { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<JobProfileType> JobProfileType { get; set; }
        public DbSet<JobProfile> JobProfile { get; set; }
        public DbSet<JobVacancy> JobVancancy { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<CandidateJobProfileMapping> CandidateJobProfileMapping { get; set; }
    }
}
