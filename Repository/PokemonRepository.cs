using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class PokemonRepository: IRepository<Pokemon>
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

    public void Save()
    {
        throw new NotImplementedException();
    }
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
