using PokemonApp.Models;

namespace PokemonApp.Interfaces;

public interface IReviewRepository: IRepository<Review>
{
    bool CreateWithRelations(int reviewerId, int pokemonId, Review review);
    bool DeleteReviews(ICollection<Review> reviews);
    ICollection<Review> GetReviewsByPokemonId(int id);
}