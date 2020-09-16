using System;
using System.Collections.Generic;
using System.Text;
using EjadaAssignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EjadaAssignment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        
    }
}