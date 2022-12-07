using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Utils;
using static System.String;

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

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Review))]
    [ProducesResponseType(400)]
    public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokemonId, [FromBody] ReviewDto? dto)
    {
        if (dto == null)
            return BadRequest(ModelState);

        var review = _reviewRepository.GetAll()
            .FirstOrDefault(r => string.Equals(r.Title.Trim(), dto.Title.Trim(),
                StringComparison.CurrentCultureIgnoreCase));

        if (review != null)
        {
            ModelState.AddModelError("", "Review is already exists");
            return StatusCode(422, ModelState);
        }

        var reviewMap = _mapper.Map<Review>(dto);
        if (!_reviewRepository.CreateWithRelations(reviewerId, pokemonId, reviewMap))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return StatusCode(500, ModelState);
        }

        return StatusCode(201, reviewMap);
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
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateReview(int id, [FromBody] ReviewDto? dto)
    {
        if (dto == null || !ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!_reviewRepository.Exists(id))
            return NotFound();
        
        var reviewMap = _mapper.Map<Review>(dto);

        if (!_reviewRepository.Update(id, reviewMap))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult DeleteReview(int id)
    {
        if (!_reviewRepository.Exists(id))
            return NotFound();

        if (!_reviewRepository.DeleteById(id))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return BadRequest(ModelState);
        }
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return NoContent();
    } 
}