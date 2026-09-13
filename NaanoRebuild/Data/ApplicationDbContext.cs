using Microsoft.EntityFrameworkCore;
using NaanoMVP.Models;

namespace NaanoMVP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Creator> Creators => Set<Creator>();
        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<CampaignCreator> CampaignCreators => Set<CampaignCreator>();
        public DbSet<CampaignMetric> CampaignMetrics => Set<CampaignMetric>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Creator>()
                .Property(c => c.EngagementRate)
                .HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Creator>()
                .Property(c => c.Price)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Campaign>()
                .Property(c => c.Budget)
                .HasColumnType("decimal(12,2)");

            modelBuilder.Entity<CampaignMetric>()
                .Property(m => m.Spend)
                .HasColumnType("decimal(12,2)");

            modelBuilder.Entity<CampaignCreator>()
                .HasOne(cc => cc.Campaign)
                .WithMany(c => c.CampaignCreators)
                .HasForeignKey(cc => cc.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CampaignCreator>()
                .HasOne(cc => cc.Creator)
                .WithMany(c => c.CampaignCreators)
                .HasForeignKey(cc => cc.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CampaignMetric>()
                .HasOne(m => m.Campaign)
                .WithMany(c => c.CampaignMetrics)
                .HasForeignKey(m => m.CampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent the same creator being added twice to the same campaign
            modelBuilder.Entity<CampaignCreator>()
                .HasIndex(cc => new { cc.CampaignId, cc.CreatorId })
                .IsUnique();
        }
    }
}
