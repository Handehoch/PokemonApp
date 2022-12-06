using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class OwnerRepository: IOwnerRepository
{
    private readonly DataContext _context;

    public OwnerRepository(DataContext context)
    {
        _context = context;
    }
    
    public Owner Create(Owner dto)
    {
        throw new NotImplementedException();
    }

    public Owner GetById(int id)
    {
        return _context.Owners.First(o => o.Id == id);
    }

    public ICollection<Owner> GetAll()
    {
        return _context.Owners.ToList();
    }
    
    public ICollection<Owner> GetOwnersByPokemonId(int id)
    {
        return _context.PokemonOwners
            .Where(po => po.PokemonId == id)
            .Select(po => po.Owner)
            .ToList();
    }

    public ICollection<Pokemon> GetPokemonsByOwnerId(int id)
    {
        return _context.PokemonOwners
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
        return _context.Owners.Any(o => o.Id == id);
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}