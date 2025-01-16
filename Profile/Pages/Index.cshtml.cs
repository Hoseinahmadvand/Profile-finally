
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Profile.Models;
using Profile.Models.AboutUsAgg;
using Profile.Models.Common;
using Profile.Models.ConnectUsAgg;
using Profile.Models.GalleryAgg;
using Profile.Models.ProjectAgg;
using Profile.Models.SetingSiteAgg;
using Profile.Models.SkillAgg;
using Profile.Models.VideoAgg;
using Profile.Services.Interfaces;
using Profile.ViewModels;

namespace Profile.Pages;

public class IndexModel : PageModel
{
    private readonly IVideoService _videoService;
    private readonly IProjectService _projectService;
    private readonly IGalleryImageService _galleryService;
    private readonly ISkillService _skillService;
    private readonly IAboutUsService _aboutUsService;
    private readonly ISiteVisitService _siteVisitService;
    private readonly ICommentService _commentService;
    private readonly IContactUsService _contactUsService;
    private readonly IFooterService _footerService;
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly ICaptchaValidator _captchaValidator;
    private readonly ISiteSettingService _siteSettingService;

    public IndexModel(
        IVideoService videoService,
        IProjectService projectService,
        IGalleryImageService galleryService,
        ISkillService skillService,
        IAboutUsService aboutUsService,
        IStringLocalizer<SharedResource> localizer,
        ISiteVisitService siteVisitService,
        ICommentService commentService,
        IContactUsService contactUsService,
        ICaptchaValidator captchaValidator,
        IFooterService footerService,
        ISiteSettingService siteSettingService)
    {
        _videoService = videoService;
        _projectService = projectService;
        _galleryService = galleryService;
        _skillService = skillService;
        _aboutUsService = aboutUsService;
        _localizer = localizer;
        _siteVisitService = siteVisitService;
        _commentService = commentService;
        _contactUsService = contactUsService;
        _captchaValidator = captchaValidator;
        _footerService = footerService;
        _siteSettingService = siteSettingService;
    }

    public string CurrentCulture { get; set; }
    public List<Video> Videos { get; set; }

    public List<Project> Projects { get; set; }
    public string ProjectsTitle { get; set; }


    public List<GalleryImage> GalleryImages { get; set; }
    public string GalleryImagesTitle { get; set; }

    public List<Skill> Skills { get; set; }
    public string SkillsTitle { get; set; }


    public AboutUs AboutUs { get; set; }
    public string AboutUsTitle { get; set; }

    public ContactUs ContactUs { get; set; }
    public string ContactUsTitle { get; set; }

    public List<Footer> Footers { get; set; }

    public int VisitCount { get; set; }
    public string VisitCountTitle { get; set; }

    [BindProperty]
    public string Name { get; set; }
    public string NameTitle { get; set; }
    [BindProperty]
    public string Email { get; set; }
    public string EmailTitle { get; set; }
    [BindProperty]
    public string Message { get; set; }
    public string MessageTitle { get; set; }

    public string SendTitle { get; set; }
    public string AddressTitle { get; set; }
    public string PhoneTitle { get; set; }
    public string DescriptionTitle { get; set; }
    public string SuccssMessageTitle { get; set; }
    
    public siteSetting siteSettings { get; set; }
    public async Task OnGetAsync()
    {
        await LoadPageDataAsync();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var recaptchaResponse = Request.Form["g-recaptcha-response"];
            var isValidCaptcha = await ValidateRecaptchaAsync(recaptchaResponse);

            //if (!isValidCaptcha)
            //{
            //    // اگر کپچا معتبر نبود، مدل داده‌ها را دوباره بارگذاری کن
            //    ModelState.AddModelError(string.Empty, "Invalid CAPTCHA.");
            //    await LoadPageDataAsync();
            //    return Page(); // بازگشت به همان صفحه
            //}

            // ساخت یک کامنت جدید
            var comment = new Comment()
            {
                CreateTime = DateTime.Now,
                Name = Name,
                Email = Email,
                Message = Message
            };

            // اضافه کردن کامنت به دیتابیس
            await _commentService.AddAsync(comment);

            await LoadPageDataAsync();
            string msg = "";
            if (CurrentCulture == "fa")
               msg= "علمیات با موفقیت انجام شد";
            if (CurrentCulture == "ar")
               msg = "تمت العملية بنجاح";
            if (CurrentCulture == "de")
              msg = "Vorgang erfolgreich abgeschlossen";
            TempData["SuccessMessage"] = msg;
            return RedirectToPage();
        }
        catch (Exception ex)
        {
            // اگر خطایی رخ داد، خطا را لاگ کن و داده‌ها را دوباره بارگذاری کن
            TempData["ErrorMessage"] = "recaptcha error connection to server";
            await LoadPageDataAsync();
            return Page();
        }
    }

    public IActionResult OnPostSetLanguage(string culture)
    {
        try
        {
            Response.Cookies.Append(
         CookieRequestCultureProvider.DefaultCookieName,
         CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
         new CookieOptions
         {
             Expires = DateTimeOffset.UtcNow.AddYears(1),
             Secure = false, // اگر روی HTTPS اجرا می‌شود
             SameSite = SameSiteMode.Lax
         }
     );

            Console.WriteLine("Cookie Created Successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Creating Cookie: {ex.Message}");
        }
        return RedirectToPage();
    }


    private async Task<bool> ValidateRecaptchaAsync(string recaptchaResponse)
    {
        var secretKey = "6LdwRaQqAAAAAF_HnabBYzlRrf5JxzOr5ovcVXMT";
        using var httpClient = new HttpClient();
        var response = await httpClient.PostAsync(
            $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={recaptchaResponse}",
            null);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var captchaResult = JsonConvert.DeserializeObject<RecaptchaResponse>(jsonResponse);
        return captchaResult != null && captchaResult.Success && captchaResult.Score >= 0.5; // بررسی امتیاز (Score)
    }

    private async Task<List<GalleryImage>> ShowPictureWithLanguage(string lang)
    {
        var pic = (await _galleryService.GetGalleryImagesAsync()).ToList();
        var result = new List<GalleryImage>();
        if (lang == "fa")
            result = pic.Where(p => p.lang == Language.fa).ToList();
        if (lang == "ar")
            result = pic.Where(p => p.lang == Language.ar).ToList();
        if (lang == "de")
            result = pic.Where(p => p.lang == Language.de).ToList();
        return result;
    }
    private async Task<List<Video>> ShowVideoWithLanguage(string lang)
    {
        var vid = (await _videoService.GetAllAsync()).ToList();

        var result = lang switch
        {
            "fa" => vid.Where(p => p.lang == Language.fa).ToList(),
            "ar" => vid.Where(p => p.lang == Language.ar).ToList(),
            "de" => vid.Where(p => p.lang == Language.de).ToList(),
            _ => new List<Video>()
        };
        return result;
    }
    private async Task<List<Project>> ShowProjectWithLanguage(string lang)
    {
        var pro = (await _projectService.GetAllAsync()).ToList();
        var result = new List<Project>();
        if (lang == "fa")
            result = pro.Where(p => p.lang == Language.fa).ToList();
        if (lang == "ar")
            result = pro.Where(p => p.lang == Language.ar).ToList();
        if (lang == "de")
            result = pro.Where(p => p.lang == Language.de).ToList();
        return result;
    }
    private async Task<List<Skill>> ShowSkillWithLanguage(string lang)
    {
        var sk = (await _skillService.GetAllAsync()).ToList();
        var result = new List<Skill>();
        if (lang == "fa")
            result = sk.Where(p => p.lang == Language.fa).ToList();
        if (lang == "ar")
            result = sk.Where(p => p.lang == Language.ar).ToList();
        if (lang == "de")
            result = sk.Where(p => p.lang == Language.de).ToList();
        return result;
    }  
    private async Task<List<Footer>> ShowFooterWithLanguage(string lang)
    {
        var sk = (await _footerService.GetAllAsync()).ToList();
        var result = new List<Footer>();
        if (lang == "fa")
            result = sk.Where(p => p.lang == Language.fa).ToList();
        if (lang == "ar")
            result = sk.Where(p => p.lang == Language.ar).ToList();
        if (lang == "de")
            result = sk.Where(p => p.lang == Language.de).ToList();
        return result;
    }   
    private async Task<siteSetting> ShowsettingWithLanguage(string lang)
    {
        var sk = await _siteSettingService.GetByIdAsync(1);
        var result = new siteSetting();
        if (lang == "fa")
        {
            result.ShowProjects = sk.ShowProjectsFa;
            result.ShowSkills = sk.ShowSkillsFa;
            result.ShowGallery= sk.ShowGalleryFa;
            result.ShowContactUs= sk.ShowContactUsFa;
        }
        if (lang == "ar")
        {
            result.ShowProjects = sk.ShowProjectsAr;
            result.ShowSkills = sk.ShowSkillsAr;
            result.ShowGallery = sk.ShowGalleryAr;
            result.ShowContactUs = sk.ShowContactUsAr;
        }
        if (lang == "de")
        {
            result.ShowProjects = sk.ShowProjectsDe;
            result.ShowSkills = sk.ShowSkillsDe;
            result.ShowGallery = sk.ShowGalleryDe;
            result.ShowContactUs = sk.ShowContactUsDe;
        }
        return result;
    }
    private async Task LoadPageDataAsync()
    {
        CurrentCulture = HttpContext
        .Features.Get<IRequestCultureFeature>()?
        .RequestCulture.Culture.Name ?? "fa";

        await _siteVisitService.IncrementVisitCountAsync();

        VisitCount = await _siteVisitService.GetVisitCountAsync();

        Videos = await ShowVideoWithLanguage(CurrentCulture);
        Projects = await ShowProjectWithLanguage(CurrentCulture);
        GalleryImages = await ShowPictureWithLanguage(CurrentCulture);
        Skills = await ShowSkillWithLanguage(CurrentCulture);
        siteSettings= await ShowsettingWithLanguage(CurrentCulture);

        AboutUs = await _aboutUsService.GetAboutUsWithTranslationsAsync();
        ContactUs = await _contactUsService.GetContactUsWithTranslationsAsync();
        Footers = await ShowFooterWithLanguage(CurrentCulture);

        ContactUsTitle = _localizer["ContactUs_Title"];
        ProjectsTitle = _localizer["Projects_Title"];
        SkillsTitle = _localizer["Skills_Title"];
        VisitCountTitle = _localizer["SiteVisit_Count"];
        AboutUsTitle = _localizer["AboutUs_Title"];
        GalleryImagesTitle = _localizer["Gallery_Title"];
        ContactUsTitle = _localizer["ContactUs_Title"];
        SendTitle = _localizer["Send_Title"];
        NameTitle = _localizer["Name_Title"];
        EmailTitle = _localizer["Email_Title"];
        MessageTitle = _localizer["Message_Title"];
        AddressTitle = _localizer["Address_Title"];
        PhoneTitle = _localizer["Phone_Title"];
        DescriptionTitle = _localizer["Description_Title"];
        SuccssMessageTitle = _localizer["SuccessMessage_Title"];
        

    }

    public class siteSetting()
    {
        public bool ShowSkills { get; set; }
        public bool ShowProjects { get; set; }
        public bool ShowGallery { get; set; }
        public bool ShowContactUs { get; set; }
    }
}
