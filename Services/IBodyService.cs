using Fitness_Tracker.ViewModels.Body;

namespace Fitness_Tracker.Services
{
    public interface IBodyService
    {
        Task CreateAsync(CreateBodyInputModel input, string userId);

        int GetCount();

        Body GetBody<Body>(string userId);

        Task DeleteAsync(int id);
    }
}
