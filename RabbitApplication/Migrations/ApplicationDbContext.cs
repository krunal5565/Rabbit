using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RabbitApplication.Entity;
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
        public DbSet<JobProfile> JobProfile { get; set; }
        public DbSet<LoginDetails> LoginDetails { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<CandidateJobProfileMapping> CandidateJobProfileMapping { get; set; }
        public DbSet<CandidateFile> CandidateFiles { get; set; }
    }
}
