using Microsoft.EntityFrameworkCore;
using Profile.Models;
using Profile.Models.AboutUsAgg;
using Profile.Models.ConnectUsAgg;
using Profile.Models.GalleryAgg;
using Profile.Models.ProjectAgg;
using Profile.Models.SetingSiteAgg;
using Profile.Models.SkillAgg;
using Profile.Models.UserAgg;
using Profile.Models.VideoAgg;

namespace Profile.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<Video> Videos { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<AboutUs> AboutUs { get; set; }
    public DbSet<AboutUsTranslation> AboutUsTranslations { get; set; }

    public DbSet<ContactUs> ContactUs { get; set; }
    public DbSet<ContactUsTranslation> ContactUsTranslations { get; set; }

    public DbSet<GalleryImage> GalleryImages { get; set; }
    public DbSet<SiteSetting> SiteSettings { get; set; }
    public DbSet<SiteVisit> SiteVisits { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    //public DbSet<BlockedCountry> BlockedCountries { get; set; }
    public DbSet<BlockedIp> BlockedIps { get; set; }

    public DbSet<Footer> Footers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AboutUs>()
            .HasMany(a => a.Translations)
            .WithOne(a => a.AboutUs)
            .HasForeignKey(a => a.AboutUsId);

        modelBuilder.Entity<ContactUs>()
            .HasMany(a => a.Translations)
            .WithOne(a => a.ContactUs)
            .HasForeignKey(a => a.ContactUsId);

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ContactUs>().HasData(new ContactUs
        {
            Id = 1,
            Address = "Default Address",
            Email = "default@example.com",
            Phone = "+123456789"
        });

        // داده پیش‌فرض برای ContactUsTranslation
        modelBuilder.Entity<ContactUsTranslation>().HasData(
            new ContactUsTranslation
            {
                Id = 1,
                ContactUsId = 1,
                Language = "fa",
                Description = "توضیحات پیش‌فرض به زبان فارسی"
            },
            new ContactUsTranslation
            {
                Id = 2,
                ContactUsId = 1,
                Language = "ar",
                Description = "الوصف الافتراضي باللغة العربية"
            },
            new ContactUsTranslation
            {
                Id = 3,
                ContactUsId = 1,
                Language = "de",
                Description = "Standardbeschreibung auf Deutsch"
            }
        );

        // داده پیش‌فرض برای SiteSetting
        modelBuilder.Entity<SiteSetting>().HasData(new SiteSetting
        {
            Id = 1,
            ShowContactUsAr=true,
            ShowContactUsDe=true,
            ShowContactUsFa=true,
            ShowGalleryAr=true,
            ShowGalleryDe=true,
            ShowGalleryFa=true,
            ShowProjectsAr=true,
            ShowProjectsDe=true,
            ShowProjectsFa=true,
            ShowSkillsAr=true,
            ShowSkillsDe=true,
            ShowSkillsFa=true,
            TitleSiteAr ="العربیه",
            TitleSiteDe="Germany",
            TitleSiteFa="فارسی",
            LogoPath=""
        });

        modelBuilder.Entity<SiteVisit>().HasData(new SiteVisit
        {
            Id = 1,
            VisitCount = 0
        });

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", // 123456
            Role = "Admin"
        });
    }

}
