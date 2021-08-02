using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AdDbContext : DbContext
    {
        public AdDbContext(DbContextOptions options) : base(options)
        { 
        
        }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdTag> AdTags { get; set; }
        public DbSet<AdType> AdTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Tag> Tags{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdType>().HasData(
                new AdType[]
                {
                    new AdType {Id = 1, Name = "TextAd" },
                    new AdType {Id = 2, Name = "HtmlAd" },
                    new AdType {Id = 3, Name = "BannerAd" },
                    new AdType {Id = 4, Name = "VideoAd" }
                });
            modelBuilder.Entity<Category>().HasData(
                new Category[]
                {
                    new Category {Id = 1, Name = "Toys" },
                    new Category {Id = 2, Name = "Online Lessons" }
                });
            #region Ad
            modelBuilder.Entity<Ad>()
                .HasOne(atp => atp.AdType)
                .WithMany(a => a.Ads)
                .HasForeignKey(atp => atp.AdTypeId);
            modelBuilder.Entity<Ad>()
                .HasOne(cat => cat.Category)
                .WithMany(a => a.Ads)
                .HasForeignKey(cat => cat.CategoryId);
            modelBuilder.Entity<Ad>()
                .HasOne(con => con.Content)
                .WithMany(a => a.Ads)
                .HasForeignKey(con => con.ContentId);
            modelBuilder.Entity<Ad>()
                .Property(a => a.CurrentAd)
                .HasDefaultValue(false);
            #endregion
            #region Ad_Tag
            modelBuilder.Entity<AdTag>()
                .HasOne(a => a.Ad)
                .WithMany(at => at.AdTags)
                .HasForeignKey(a => a.Ad_Id);
            modelBuilder.Entity<AdTag>()
                .HasOne(t => t.Tag)
                .WithMany(at => at.AdTags)
                .HasForeignKey(a => a.Tag_Id);
            #endregion
        }


    }
}
