using Profile.Models.GalleryAgg;

namespace Profile.Repository.Interfaces;

public interface IGalleryImageRepository : IGenericRepository<GalleryImage>
{
    Task<IEnumerable<GalleryImage>> GetGalleryImagesAsync();
}
