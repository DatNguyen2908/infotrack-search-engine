using Infotrack.Sales.SearchEngine.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotrack.Sales.SearchEngine.EFCore
{
    public class SearchEngineContext : DbContext
    {
        public SearchEngineContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SearchHistory> SearchHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchHistory>().ToTable("SearchHistories").HasKey(x => x.Id);

            modelBuilder.Entity<SearchHistory>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<SearchHistory>().Property(x => x.Url).HasMaxLength(255);

            modelBuilder.Entity<SearchHistory>().Property(x => x.Keyword).HasMaxLength(255);

            modelBuilder.Entity<SearchHistory>().Property(x => x.SearchDate)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd()
                .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<SearchHistory>().HasIndex(x => new { x.Url, x.Keyword, x.SearchDate });


            base.OnModelCreating(modelBuilder);
        }
    }
}
