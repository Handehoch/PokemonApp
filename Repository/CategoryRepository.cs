using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class CategoryRepository: RepositoryBase, ICategoryRepository
{
    public CategoryRepository(DataContext context) : base(context)
    {
        
    }

    public bool Create(Category dto)
    {
        Context.Categories.Add(dto);
        return Save();
    }

    public Category GetById(int id)
    {
        return Context.Categories.First(c => c.Id == id);
    }

    public ICollection<Category> GetAll()
    {
        return Context.Categories.ToList();
    }

    public bool Update(int id, Category dto)
    {
        dto.Id = id;
        Context.Categories.Update(dto);
        return Save();
    }

    public Category DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public ICollection<Pokemon> GetPokemonByCategoryId(int id)
    {
        return Context.PokemonCategories
            .Where(e => e.CategoryId == id)
            .Select(c => c.Pokemon)
            .ToList();
    }
    
    public bool Exists(int id)
    {
        return Context.Categories.Any(c => c.Id == id);
    }
}
