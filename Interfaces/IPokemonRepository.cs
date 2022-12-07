using PokemonApp.Models;

namespace PokemonApp.Interfaces;

public interface IPokemonRepository : IRepository<Pokemon>
{
    bool CreateWithRelations(int ownerId, int categoryId, Pokemon pokemon);
    Pokemon GetByName(string name);
    double GetRatingById(int id);
}