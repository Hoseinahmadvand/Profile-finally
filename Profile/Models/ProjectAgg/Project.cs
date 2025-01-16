using Profile.Models.Common;

namespace Profile.Models.ProjectAgg;

public class Project : BaseEntity
{
    public string ImageName { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Language lang { get; set; }
}
