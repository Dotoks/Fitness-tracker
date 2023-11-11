using Fitness_Tracker.Data;
using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels.Body;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Services
{
    public class BodyService : IBodyService
    {
        private readonly ApplicationDbContext _context;

        public BodyService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateBodyInputModel input, string userId)
        {
            var currentBodyModel = _context.Body
            .Where(b => b.UserID == userId && b.EffectiveThroughDate == DateTime.MaxValue) // Fetch the current record
            .OrderByDescending(b => b.EffectiveFromDate) // Order by StartDate to get the latest
            .FirstOrDefault();

            if(currentBodyModel != null)
            {
                currentBodyModel.EffectiveThroughDate = DateTime.Now;
                currentBodyModel.CurrentRecordIndicator = false;
                await _context.SaveChangesAsync();

            }

            var body = new Body
            {
                UserID = userId,
                Weight = input.Weight,
                Height = input.Height,
                Age = input.Age,
                EffectiveFromDate = DateTime.UtcNow,
                EffectiveThroughDate = DateTime.MaxValue,
                CurrentRecordIndicator = true
            };

            await _context.AddAsync(body);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Body GetBody<Body>(string userId)
        {
            var currentBody = _context.Body.Where(x => x.UserID == userId && x.EffectiveThroughDate == DateTime.MaxValue).FirstOrDefault();

            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }
    }
}
