using Profile.Models;
using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
namespace Profile.Services.Implementations;

public class CommentService : GenericService<Comment>, ICommentService 
{
    private readonly ICommentRepository _commentRepository;
    public CommentService(ICommentRepository commentRepository):base(commentRepository) 
    {
        _commentRepository = commentRepository;
    }
}



