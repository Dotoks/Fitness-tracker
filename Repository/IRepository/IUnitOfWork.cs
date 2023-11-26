namespace Fitness_Tracker.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBodyRepository Body { get; }
        IIngredientRepository Ingredient { get; }
        IRecipeIngredientRepository RecipeIngredient { get; }
        IRecipeRepository Recipe { get; }
        IUserRepository User { get; }
        IInstructionRepository Instruction { get; }
        IDailyMacrosRepository DailyMacros { get; }

        void Save();
        Task SaveAsync();
        
    }
}
