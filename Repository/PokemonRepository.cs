using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class PokemonRepository : RepositoryBase, IPokemonRepository
{
    public PokemonRepository(DataContext context) : base(context)
    {
        
    }

    public bool Create(Pokemon dto)
    {
        throw new NotImplementedException();
    }

    public Pokemon GetById(int id)
    {
        return Context.Pokemon.First(p => p.Id == id);
    }

    public Pokemon GetByName(string name)
    {
        return Context.Pokemon.First(p => p.Name == name);
    }

    public double GetRatingById(int id)
    {
        var reviews = Context.Reviews.Where(r => r.Pokemon.Id == id).ToList();
        return !reviews.Any() ? 0 : (double) reviews.Sum(r => r.Rating) / reviews.Count;
    }

    public ICollection<Pokemon> GetAll()
    {
        return Context.Pokemon.ToList();
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
        return Context.Pokemon.Any(p => p.Id == id);
    }
}