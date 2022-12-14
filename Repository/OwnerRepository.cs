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
    
    public bool Update(int id, Owner dto)
    {
        dto.Id = id;
        Context.Owners.Update(dto);
        return Save();
    }

    public bool DeleteById(int id)
    {
        var owner = GetById(id);
        Context.Owners.Remove(owner);
        return Save();
    }

    public bool Exists(int id)
    {
        return Context.Owners.Any(o => o.Id == id);
    }
}