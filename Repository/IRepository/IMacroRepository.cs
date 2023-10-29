using Fitness_Tracker.Models;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IMacroRepository : IRepository<Macro>
    {
        void Update(Macro obj);
    }
}
