using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.VideoAgg;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class VideoRepository : GenericRepository<Video>, IVideoRepository
{
    public VideoRepository(ApplicationContext context):base(context)
    {
        
    }
  
}
