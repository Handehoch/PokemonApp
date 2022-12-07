using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class ReviewRepository: RepositoryBase, IReviewRepository
{
    public ReviewRepository(DataContext context) : base(context)
    {
        
    }
    
    public bool Create(Review dto)
    {
        Context.Reviews.Add(dto);
        return Save();
    }
    public bool CreateWithRelations(int reviewerId, int pokemonId, Review review)
    {
        var reviewer = Context.Reviewers.FirstOrDefault(r => r.Id == reviewerId);
        var pokemon = Context.Pokemon.FirstOrDefault(p => p.Id == pokemonId);

        review.Reviewer = reviewer;
        review.Pokemon = pokemon;

        return Create(review);
    }

    public Review GetById(int id)
    {
        return Context.Reviews.First(r => r.Id == id);
    }

    public ICollection<Review> GetAll()
    {
        return Context.Reviews.ToList();
    }

    public ICollection<Review> GetReviewsByPokemonId(int id)
    {
        return Context.Reviews.Where(r => r.Pokemon.Id == id).ToList();
    }

    public Review UpdateById(int id, Review dto)
    {
        throw new NotImplementedException();
    }

    public Review DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        return Context.Reviews.Any(r => r.Id == id);
    }
}