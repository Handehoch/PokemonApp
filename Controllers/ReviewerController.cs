using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewerController: Controller
{
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IMapper _mapper;

    public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
    {
        _reviewerRepository = reviewerRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Reviewer>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetReviewers()
    {
        var reviews = _mapper.Map<ICollection<ReviewerDto>>(_reviewerRepository.GetAll());
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(reviews);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Reviewer))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetReviewer(int id)
    {
        if (!_reviewerRepository.Exists(id))
            return NotFound();

        var review = _mapper.Map<ReviewerDto>(_reviewerRepository.GetById(id));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(review);
    }

    [HttpGet("pokemon/{reviewerId:int}")]
    [ProducesResponseType(200, Type = typeof(Review))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetReviewsByReviewer(int reviewerId)
    {
        var reviews = _mapper.Map<ICollection<ReviewDto>>(_reviewerRepository.GetReviewsByReviewerId(reviewerId));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(reviews);
    }
}