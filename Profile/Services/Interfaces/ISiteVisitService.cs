namespace Profile.Services.Interfaces;

public interface ISiteVisitService
{
    Task IncrementVisitCountAsync();
    Task<int> GetVisitCountAsync();
}



