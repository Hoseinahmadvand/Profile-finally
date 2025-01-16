using Profile.Data;
using Profile.Models;
using Profile.Repository.Interfaces;

namespace Profile.Repository.Implementations;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository 
{
    public CommentRepository(ApplicationContext context) : base(context) { }
}

