using Fitness_Tracker.Models;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IInstructionRepository: IRepository<Instruction>
    {
        void Update(Instruction instruction);
    }
}
