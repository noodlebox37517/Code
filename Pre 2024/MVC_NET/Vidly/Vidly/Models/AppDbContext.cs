using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Vidly.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {

        }
     
        public DbSet<Customer> Customers { set; get; }
        public DbSet<Movie> Movies { get; set; }
    }
}