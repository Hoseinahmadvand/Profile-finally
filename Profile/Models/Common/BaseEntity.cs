namespace Profile.Models.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime? UpdateTime { get; set; }
    public bool IsActive { get; set; }=true;

}
