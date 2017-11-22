using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HamperWeb.Models;

namespace HamperWeb.Services
{
    public class MyDbContext : IdentityDbContext
    {
        public DbSet<Hamper> TblHamper { get; set; }
        public DbSet<Order> TblOrder { get; set; }
        public DbSet<Category> TblCategory { get; set; }
        public DbSet<OrderState> TblOrderState { get; set; }
        public DbSet<Profile> TblProfile { get; set; }
        public DbSet<Address> TblAddress { get; set; }
        public DbSet<OrderDetail> TblOrderDetail { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB ; Database= HamperBase ; Trusted_Connection=True;");
        }
    }
}
