using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productapp.Models
{
    public class productcontext:DbContext
    {
        public productcontext(DbContextOptions options) : base(options)
        { }
        public DbSet<login> logins { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<product> products { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<cart> carts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<login>().HasKey(c => c.login_id);
            modelBuilder.Entity<login>().Property(c => c.login_id).ValueGeneratedOnAdd();
            modelBuilder.Entity<user>().HasKey(s => s.user_id);
            modelBuilder.Entity<user>().Property(s => s.user_id).ValueGeneratedOnAdd();
            modelBuilder.Entity<product>().HasKey(s => s.product_id);
            modelBuilder.Entity<product>().Property(s => s.product_id).ValueGeneratedOnAdd();
            modelBuilder.Entity<category>().HasKey(s => s.category_id);
            modelBuilder.Entity<category>().Property(s => s.category_id).ValueGeneratedOnAdd();
            modelBuilder.Entity<cart>().HasKey(s => s.cart_id);
            modelBuilder.Entity<cart>().Property(s => s.cart_id).ValueGeneratedOnAdd();

            modelBuilder.Entity<login>().HasOne(s => s.user).WithMany(c => c.logins).HasForeignKey(s => s.u_id);
            modelBuilder.Entity<product>().HasOne(s => s.category).WithMany(c => c.products).HasForeignKey(s => s.cat_id);
            modelBuilder.Entity<cart>().HasOne(s => s.user).WithMany(c => c.carts).HasForeignKey(s => s.u_id);
            modelBuilder.Entity<cart>().HasOne(s => s.product).WithMany(c => c.carts).HasForeignKey(s => s.prdct_id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
