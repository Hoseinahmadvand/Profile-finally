using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.GalleryAgg;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class GalleryImageRepository : GenericRepository<GalleryImage>, IGalleryImageRepository
{
    public GalleryImageRepository(ApplicationContext context) : base(context) { }

    public async Task<IEnumerable<GalleryImage>> GetGalleryImagesAsync()
    {
        return await _context.Set<GalleryImage>().ToListAsync();
    }
}
