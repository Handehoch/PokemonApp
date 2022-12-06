using PokemonApp.Models;

namespace PokemonApp.Interfaces;

public interface IReviewRepository: IRepository<Review>
{
    ICollection<Review> GetReviewsByPokemonId(int id);
}