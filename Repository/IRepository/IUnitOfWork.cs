namespace Fitness_Tracker.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBodyRepository Body { get; }
        IIngredientRepository Ingredient { get; }
        IMacroRepository Macro { get; }
        IRecipeRepository Recipe { get; }
        IUserRepository User { get; }

        void Save();
        Task SaveAsync();
    }
}
