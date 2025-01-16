using Profile.Models.Common;

namespace Profile.Models.SkillAgg;

public class Skill : BaseEntity
{
    public int Persent { get; set; }
    public string? Title { get; set; }
    public Language lang { get; set; }

}
