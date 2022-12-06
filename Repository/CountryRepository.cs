using AutoMapper;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class CountryRepository: ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context)
    {
        _context = context;
    }

    public Country Create(Country dto)
    {
        throw new NotImplementedException();
    }

    public Country GetById(int id)
    {
        return _context.Countries.First(c => c.Id == id);
    }

    public ICollection<Country> GetAll()
    {
        return _context.Countries.ToList();
    }

    public Country UpdateById(int id, Country dto)
    {
        throw new NotImplementedException();
    }

    public Country DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public Country GetCountryByOwnerId(int id)
    {
        return _context.Owners.Where(o => o.Id == id).Select(o => o.Country).First();
    }

    public ICollection<Owner> GetOwnersByCountryId(int id)
    {
        return _context.Owners.Where(c => c.Country.Id == id).ToList();
    }
    
    public bool Exists(int id)
    {
        return _context.Countries.Any(c => c.Id == id);
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