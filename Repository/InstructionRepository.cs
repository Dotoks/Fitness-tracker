using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.Repository.IRepository;

namespace Fitness_Tracker.Repository
{
    public class InstructionRepository : Repository<Instruction>, IInstructionRepository
    {
        private ApplicationDbContext _db;
        public InstructionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Instruction obj)
        {
            _db.Instructions.Update(obj);
        }
    }
}
