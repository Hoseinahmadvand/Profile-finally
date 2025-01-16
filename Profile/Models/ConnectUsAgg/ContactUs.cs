namespace Profile.Models.ConnectUsAgg;

public class ContactUs
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public List<ContactUsTranslation> Translations { get; set; } = new();
}
