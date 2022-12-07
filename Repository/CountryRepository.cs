using AutoMapper;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class CountryRepository: RepositoryBase, ICountryRepository
{

    public CountryRepository(DataContext context) : base(context)
    {
    }

    public bool Create(Country dto)
    {
        Context.Countries.Add(dto);
        return Save();
    }

    public Country GetById(int id)
    {
        return Context.Countries.First(c => c.Id == id);
    }

    public ICollection<Country> GetAll()
    {
        return Context.Countries.ToList();
    }

    public bool Update(int id, Country dto)
    {
        dto.Id = id;
        Context.Countries.Update(dto);
        return Save();
    }

    public Country DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public Country GetCountryByOwnerId(int id)
    {
        return Context.Owners.Where(o => o.Id == id).Select(o => o.Country).First();
    }

    public ICollection<Owner> GetOwnersByCountryId(int id)
    {
        return Context.Owners.Where(o => o.Country.Id == id).ToList();
    }
    
    public bool Exists(int id)
    {
        return Context.Countries.Any(c => c.Id == id);
    }
}