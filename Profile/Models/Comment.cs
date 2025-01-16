using Profile.Models.Common;

namespace Profile.Models
{
    public class Comment:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
