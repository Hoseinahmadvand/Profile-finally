using Microsoft.EntityFrameworkCore;
using Profile.Data;
using Profile.Models.SkillAgg;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class SkillRepository : GenericRepository<Skill>, ISkillRepository
{
    public SkillRepository(ApplicationContext context) : base(context) { }

  
}