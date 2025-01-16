using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.ProjectAgg;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationContext context) : base(context) { }


}

