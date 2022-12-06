using Microsoft.EntityFrameworkCore;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository;

public class ReviewerRepository: RepositoryBase, IReviewerRepository
{
    public ReviewerRepository(DataContext context) : base(context)
    {
        
    }

    public bool Create(Reviewer dto)
    {
        Context.Reviewers.Add(dto);
        return Save();
    }

    public Reviewer GetById(int id)
    {
        return Context.Reviewers
            .Where(r => r.Id == id)
            .Include(r => r.Reviews)
            .First();
    }

    public ICollection<Reviewer> GetAll()
    {
        return Context.Reviewers.ToList();
    }
    
    public ICollection<Review> GetReviewsByReviewerId(int id)
    {
        return Context.Reviews.Where(r => r.Reviewer.Id == id).ToList();
    }

    public Reviewer UpdateById(int id, Reviewer dto)
    {
        throw new NotImplementedException();
    }

    public Reviewer DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(int id)
    {
        return Context.Reviewers.Any(r => r.Id == id);
    }
}