using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ShopDienThoai.EntityFramework
{
    public partial class ShopDienThoaiDbContext : DbContext
    {
        public ShopDienThoaiDbContext()
            : base("name=ShopDienThoaiDbContext")
        {
        }

        public virtual DbSet<article> articles { get; set; }
        public virtual DbSet<banner> banners { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<contact> contacts { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<order_details> order_details { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<article>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.images)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<banner>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<banner>()
                .Property(e => e.images)
                .IsUnicode(false);

            modelBuilder.Entity<banner>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<banner>()
                .Property(e => e.targets)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.icon)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.C_address)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.pass_word)
                .IsUnicode(false);

            modelBuilder.Entity<order_details>()
                .Property(e => e.images)
                .IsUnicode(false);

            modelBuilder.Entity<order_details>()
                .Property(e => e.sku)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.address_order)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.coupon)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.images)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.sku)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.passwords)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.remember_token)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.avatar)
                .IsUnicode(false);
        }
    }
}
