using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskRequestApplication.Models
{
    public class TaskRequestApplicationDBContext:DbContext
    {
        public TaskRequestApplicationDBContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=TaskRequestAppDB;uid=sa;pwd=1234;");
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-O23KDVQ;Database=TaskRequestApplicationDB;Trusted_Connection=True;");

            base.OnConfiguring(optionsBuilder);

        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
