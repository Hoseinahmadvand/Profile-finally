using Profile.Models.Common;

namespace Profile.Models.AboutUsAgg;

public class AboutUsTranslation:BaseTranslation
{
  
    public int AboutUsId { get; set; }
    public AboutUs AboutUs { get; set; }

    public string Language { get; set; } // زبان (مانند "fa" برای فارسی)
    public string Title { get; set; } // عنوان ترجمه‌شده
    public string Description { get; set; } // توضیحات ترجمه‌شده
}
