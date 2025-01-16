using Profile.Models.Common;

namespace Profile.Models.VideoAgg;

public class Video:BaseEntity
{
    
    public string? Url { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Language lang { get; set; }

}
