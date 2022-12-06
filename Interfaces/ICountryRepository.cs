using PokemonApp.Models;

namespace PokemonApp.Interfaces;

public interface ICountryRepository: IRepository<Country>
{
    Country GetCountryByOwnerId(int id);
    ICollection<Owner> GetOwnersByCountryId(int id);
}