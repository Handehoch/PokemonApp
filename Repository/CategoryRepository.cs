using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class CategoryRepository: ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }

    public Category Create(Category dto)
    {
        throw new NotImplementedException();
    }

    public Category GetById(int id)
    {
        return _context.Categories.First(c => c.Id == id);
    }

    public ICollection<Category> GetAll()
    {
        return _context.Categories.ToList();
    }

    public Category UpdateById(int id, Category dto)
    {
        throw new NotImplementedException();
    }

    public Category DeleteById(int id)
    {
        throw new NotImplementedException();
    }


    public ICollection<Pokemon> GetPokemonByCategoryId(int id)
    {
        return _context.PokemonCategories
            .Where(e => e.CategoryId == id)
            .Select(c => c.Pokemon)
            .ToList();
    }
    
    public bool Exists(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        
    }
}
