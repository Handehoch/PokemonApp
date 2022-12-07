using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class OwnerRepository: RepositoryBase, IOwnerRepository
{
    public OwnerRepository(DataContext context): base(context)
    {
        
    }
    
    public bool Create(Owner dto)
    {
        Context.Owners.Add(dto);
        return Save();
    }

    public Owner GetById(int id)
    {
        return Context.Owners.First(o => o.Id == id);
    }

    public ICollection<Owner> GetAll()
    {
        return Context.Owners.ToList();
    }
    
    public ICollection<Owner> GetOwnersByPokemonId(int id)
    {
        return Context.PokemonOwners
            .Where(po => po.PokemonId == id)
            .Select(po => po.Owner)
            .ToList();
    }

    public ICollection<Pokemon> GetPokemonsByOwnerId(int id)
    {
        return Context.PokemonOwners
            .Where(po => po.OwnerId == id)
            .Select(po => po.Pokemon)
            .ToList();
    }

    public Owner UpdateById(int id, Owner dto)
    {
        throw new NotImplementedException();
    }

    public Owner DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        return Context.Owners.Any(o => o.Id == id);
    }
}