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
        Context.Pokemon.Add(dto);
        return Save();
    }
    
    public bool CreateWithRelations(int ownerId, int categoryId, Pokemon pokemon)
    {
        var owner = Context.Owners.First(o => o.Id == ownerId);
        var category = Context.Categories.First(c => c.Id == categoryId);

        var pokemonOwner = new PokemonOwner() { Owner = owner, Pokemon = pokemon };
        Context.PokemonOwners.Add(pokemonOwner);

        var pokemonCategory = new PokemonCategory() { Category = category, Pokemon = pokemon };
        Context.PokemonCategories.Add(pokemonCategory);

        return Create(pokemon);
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

    public bool Update(int id, Pokemon dto)
    {
        dto.Id = id;
        Context.Pokemon.Update(dto);
        return Save();
    }
    
    public bool UpdateWithRelations(int id, int ownerId, int categoryId, Pokemon pokemon)
    {
        return Update(id, pokemon);
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