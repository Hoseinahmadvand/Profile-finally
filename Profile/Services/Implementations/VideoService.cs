using Profile.Models.VideoAgg;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
namespace Profile.Services.Implementations;

public class VideoService : GenericService<Video>, IVideoService
{
    private readonly IVideoRepository _repository;
    public VideoService(IVideoRepository repository):base(repository)
    {
        _repository = repository;
    }
   
}

