using Profile.Models.Common;

namespace Profile.Models.AboutUsAgg;

public class AboutUs :BaseEntity
{
 
    public DateTime Founded { get; set; } // تاریخ تأسیس
 
    public ICollection<AboutUsTranslation> Translations { get; set; } = new List<AboutUsTranslation>();
}
