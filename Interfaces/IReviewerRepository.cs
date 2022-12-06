using PokemonApp.Models;

namespace PokemonApp.Interfaces;

public interface IReviewerRepository: IRepository<Reviewer>
{
    ICollection<Review> GetReviewsByReviewerId(int id);
}