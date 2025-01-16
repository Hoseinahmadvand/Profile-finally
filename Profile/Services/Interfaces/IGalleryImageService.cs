using Profile.Models.GalleryAgg;
namespace Profile.Services.Interfaces;

public interface IGalleryImageService : IGenericService<GalleryImage>
{
    Task<IEnumerable<GalleryImage>> GetGalleryImagesAsync();
}
