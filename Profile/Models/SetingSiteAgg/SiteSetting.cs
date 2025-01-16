namespace Profile.Models.SetingSiteAgg;

public class SiteSetting
{
    public int Id { get; set; }
    //fa
    public string? TitleSiteFa { get; set; }
    public bool ShowSkillsFa { get; set; } = true;
    public bool ShowProjectsFa { get; set; } = true;
    public bool ShowGalleryFa { get; set; } = true;
    public bool ShowContactUsFa { get; set; } = true;
    //ar
    public string? TitleSiteAr { get; set; }
    public bool ShowSkillsAr { get; set; } = true;
    public bool ShowProjectsAr { get; set; } = true;
    public bool ShowGalleryAr { get; set; } = true;
    public bool ShowContactUsAr { get; set; } = true;
    //de
    public string? TitleSiteDe { get; set; }
    public bool ShowSkillsDe { get; set; } = true;
    public bool ShowProjectsDe { get; set; } = true;
    public bool ShowGalleryDe { get; set; } = true;
    public bool ShowContactUsDe { get; set; } = true;


    public string? LogoPath { get; set; }

}
