namespace Profile.Models.ConnectUsAgg;

public class ContactUsTranslation
{
    public int Id { get; set; }
    public string Language { get; set; }
    public string Description { get; set; }

    public int ContactUsId { get; set; }
    public ContactUs ContactUs { get; set; }
}
