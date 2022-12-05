using PokemonApp.Models;

namespace PokemonApp.Interfaces;

public interface ICategoryRepository: IRepository<Category>
{
    ICollection<Pokemon> GetPokemonByCategoryId(int id);
}