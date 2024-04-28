using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using student_permit_system.PL.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace student_permit_system.PL.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<Contact> Contact { get; set; }
    }
}
