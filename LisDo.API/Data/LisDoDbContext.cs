using LisDo.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LisDo.API.Data
{
    public class LisDoDbContext : IdentityDbContext
    {
        public LisDoDbContext(DbContextOptions<LisDoDbContext> options)
            :base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region FluentApi
            //Team-User
            modelBuilder.Entity<Team_User>()
                .HasKey(k => new { k.TeamId, k.UserId });

            modelBuilder.Entity<Team_User>()
                .HasOne(u => u.User)
                .WithMany(tu => tu.Team_Users)
                .HasForeignKey(k => k.UserId);

            modelBuilder.Entity<Team_User>()
                .HasOne(t => t.Team)
                .WithMany(tu => tu.Team_Users)
                .HasForeignKey(k => k.TeamId);
            
            //Item-User
            modelBuilder.Entity<Item_User>()
                .HasKey(k => new { k.LisdoItemId, k.UserId });

            modelBuilder.Entity<Item_User>()
                .HasOne(u => u.User)
                .WithMany(tu => tu.Item_Users)
                .HasForeignKey(k => k.UserId);

            modelBuilder.Entity<Item_User>()
                .HasOne(t => t.LisdoItem)
                .WithMany(tu => tu.Item_Users)
                .HasForeignKey(k => k.LisdoItemId);

            //Team-Admin
            modelBuilder.Entity<Team_Admin>()
                .HasKey(k => new { k.TeamId, k.AdminId });

            modelBuilder.Entity<Team_Admin>()
                .HasOne(t => t.Team)
                .WithMany(a => a.Team_Admins)
                .HasForeignKey(ta => ta.TeamId);

            modelBuilder.Entity<Team_Admin>()
                .HasOne(a => a.Admin)
                .WithMany(a => a.Team_Admins)
                .HasForeignKey(ta => ta.AdminId);

            //Lisdo
            modelBuilder.Entity<Lisdo>()
                .HasMany(i => i.Items)
                .WithOne(l => l.Lisdo)
                .OnDelete(DeleteBehavior.Cascade);

            //LisdoItem
            modelBuilder.Entity<LisdoItem>()
                .HasMany(i => i.Item_Users)
                .WithOne(iu => iu.LisdoItem)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion


        }

        public DbSet<Lisdo> Lisdos { get; set; }

        public DbSet<LisdoItem> LisdoItems { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Team_User> Team_Users { get; set; }

        public DbSet<Item_User> Item_Users { get; set; }

        public DbSet<Team_Admin> Team_Admins { get; set; }
    }
}
