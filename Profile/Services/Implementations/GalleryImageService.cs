using Profile.Models.GalleryAgg;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
namespace Profile.Services.Implementations;

public class GalleryImageService : GenericService<GalleryImage>, IGalleryImageService
{
    private readonly IGalleryImageRepository _galleryImageRepository;

    public GalleryImageService(IGalleryImageRepository galleryImageRepository) : base(galleryImageRepository)
    {
        _galleryImageRepository = galleryImageRepository;
    }

    public async Task<IEnumerable<GalleryImage>> GetGalleryImagesAsync()
    {
        return await _galleryImageRepository.GetGalleryImagesAsync();
    }
}



