using PokemonApp.Models;

namespace PokemonApp.Interfaces;

public interface IPokemonRepository : IRepository<Pokemon>
{
    Pokemon GetByName(string name);
    double GetRatingById(int id);
    bool Exists(int id);
    
}