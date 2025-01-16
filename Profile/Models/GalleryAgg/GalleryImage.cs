using Profile.Models.Common;

namespace Profile.Models.GalleryAgg;

public class GalleryImage:BaseEntity
{
    public string Alt { get; set; }
    public string ImageName { get; set; }
    public string ImagePath { get; set; }
    public Language lang { get; set; }
}
