using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController: Controller
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetReviews()
    {
        var reviews = _mapper.Map<ICollection<ReviewDto>>(_reviewRepository.GetAll());
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(reviews);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Review))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetReview(int id)
    {
        if (!_reviewRepository.Exists(id))
            return NotFound();

        var review = _mapper.Map<ReviewDto>(_reviewRepository.GetById(id));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(review);
    }

    [HttpGet("pokemon/{pokemonId:int}")]
    [ProducesResponseType(200, Type = typeof(Review))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetPokemonReviews(int pokemonId)
    {
        var reviews = _mapper.Map<ICollection<ReviewDto>>(_reviewRepository.GetReviewsByPokemonId(pokemonId));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(reviews);
    }
}