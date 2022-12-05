using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class PokemonRepository: IPokemonRepository
{
    private readonly DataContext _context;

    public PokemonRepository(DataContext context)
    {
        _context = context;
    }

    public Pokemon Create(Pokemon dto)
    {
        throw new NotImplementedException();
    }

    public Pokemon GetById(int id)
    {
        return _context.Pokemon.First(p => p.Id == id);
    }

    public Pokemon GetByName(string name)
    {
        return _context.Pokemon.First(p => p.Name == name);
    }

    public double GetRatingById(int id)
    {
        var reviews = _context.Reviews.Where(r => r.Pokemon.Id == id).ToList();
        return !reviews.Any() ? 0 : (double) reviews.Sum(r => r.Rating) / reviews.Count;
    }

    public ICollection<Pokemon> GetAll()
    {
        return _context.Pokemon.ToList();
    }

    public Pokemon UpdateById(int id, Pokemon dto)
    {
        throw new NotImplementedException();
    }

    public Pokemon DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        return _context.Pokemon.Any(p => p.Id == id);
    }

    public async void Save()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
