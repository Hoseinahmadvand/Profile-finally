using Profile.Models.Common;

namespace Profile.Models.SetingSiteAgg
{
    public class Footer
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public Language lang { get; set; }
        public position Position { get; set; }
    }
    public enum position
    {
        Left,
        Right,
        center
    }
}
