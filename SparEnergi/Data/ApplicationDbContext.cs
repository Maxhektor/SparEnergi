using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SparEnergi.Models;

namespace SparEnergi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SparEnergi.Models.UserModel> UserModel { get; set; }
        public DbSet<SparEnergi.Models.MeetingModel> MeetingModel { get; set; }
        public DbSet<SparEnergi.Models.ReadingModel> ReadingModel { get; set; }
    }
}
