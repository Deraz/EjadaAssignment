using System;
using System.Collections.Generic;
using System.Text;
using EjadaAssignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EjadaAssignment.Data
{
    //using IdentityDbContext to inherit Identity options
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}
        //Employees and Departments entities
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        
    }
}