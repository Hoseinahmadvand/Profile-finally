using Microsoft.EntityFrameworkCore;
using Profile.Models.ProjectAgg;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
namespace Profile.Services.Implementations;

public class ProjectService : GenericService<Project>, IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository) : base(projectRepository)
    {
        _projectRepository = projectRepository;
    }

 
}




