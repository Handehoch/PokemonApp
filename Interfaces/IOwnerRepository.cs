using PokemonApp.Models;

namespace PokemonApp.Interfaces;

public interface IOwnerRepository: IRepository<Owner>
{
    ICollection<Owner> GetOwnersByPokemonId(int id);
    ICollection<Pokemon> GetPokemonsByOwnerId(int id);
}