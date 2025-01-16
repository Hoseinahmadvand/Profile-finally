using Profile.Repository.Implementations;
using Profile.Repository.Interfaces;
using Profile.Services.Implementations;
using Profile.Services.Interfaces;

namespace Profile;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        // ثبت سرویس‌های Repository
        services.AddTransient<IVideoRepository, VideoRepository>();
        services.AddTransient<IProjectRepository, ProjectRepository>();
        services.AddTransient<IGalleryImageRepository, GalleryImageRepository>();
        services.AddTransient<ISkillRepository, SkillRepository>();
        services.AddTransient<IAboutUsRepository, AboutUsRepository>();
        services.AddTransient<IContactUsRepository, ContactUsRepository>();
        services.AddTransient<ISiteSettingRepository, SiteSettingRepository>();
        services.AddTransient<ISiteVisitRepository, SiteVisitRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IFooterRepository, FooterRepository>();

        // ثبت سرویس‌های Service
        services.AddTransient<IVideoService, VideoService>();
        services.AddTransient<IProjectService, ProjectService>();
        services.AddTransient<IGalleryImageService, GalleryImageService>();
        services.AddTransient<ISkillService, SkillService>();
        services.AddTransient<IAboutUsService, AboutUsService>();
        services.AddTransient<IContactUsService, ContactUsService>();
        services.AddTransient<ISiteSettingService, SiteSettingService>();
        services.AddTransient<ISiteVisitService, SiteVisitService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ICommentService, CommentService>();
        services.AddTransient<IFooterService, FooterService>();

      
        return services;
    }
}
