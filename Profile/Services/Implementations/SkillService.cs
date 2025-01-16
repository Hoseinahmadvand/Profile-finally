using Profile.Models.SkillAgg;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
namespace Profile.Services.Implementations;

public class SkillService : GenericService<Skill>, ISkillService
{
    private readonly ISkillRepository _skillRepository;

    public SkillService(ISkillRepository skillRepository) : base(skillRepository)
    {
        _skillRepository = skillRepository;
    }

   
}

