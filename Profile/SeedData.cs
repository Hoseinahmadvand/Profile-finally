// Pages/Admin/AboutUs/Index.cshtml.cs
using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.AboutUsAgg;

namespace Profile
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>()))
            {
                // چک کردن وجود داده‌های قبلی
                if (context.AboutUs.Any())
                {
                    return; // اگر داده‌ای وجود دارد، هیچ کاری انجام نمی‌شود
                }

                // افزودن یک رکورد پیش‌فرض برای AboutUs
                var aboutUs = new AboutUs
                {
                    Founded = new DateTime(2000, 1, 1),
                    Translations = new[]
                    {
                        new AboutUsTranslation { Language = "fa", Title = "درباره ما", Description = "ماموریت ما ارائه خدمات با کیفیت است." },
                        new AboutUsTranslation { Language = "ar", Title = "معلومات عنا", Description = "مهمتنا هي تقديم خدمات عالية الجودة." },
                        new AboutUsTranslation { Language = "de", Title = "Über uns", Description = "Unsere Mission ist es, qualitativ hochwertige Dienstleistungen anzubieten." }
                    }
                };

                context.AboutUs.Add(aboutUs);
                context.SaveChanges();
            }
        }
    }
}
