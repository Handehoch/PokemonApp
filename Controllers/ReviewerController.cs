using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Utils;

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

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Reviewer))]
    [ProducesResponseType(400)]
    public IActionResult CreateReviewer([FromBody] ReviewerDto? dto)
    {
        if (dto == null)
            return BadRequest(ModelState);

        var reviewer = _reviewerRepository.GetAll()
            .FirstOrDefault(r => string.Equals(r.FirstName.Trim(), dto.FirstName.Trim(),
                StringComparison.CurrentCultureIgnoreCase));

        if (reviewer != null)
        {
            ModelState.AddModelError("", "Reviewer already exists");
            return StatusCode(422, ModelState);
        }

        var reviewerMap = _mapper.Map<Reviewer>(dto);

        if (!_reviewerRepository.Create(reviewerMap))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return StatusCode(500, ModelState);
        }

        return StatusCode(201, reviewerMap);
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

        var review = _mapper.Map<Reviewer>(_reviewerRepository.GetById(id));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(review);
    }

    [HttpGet("{reviewerId:int}/reviews")]
    [ProducesResponseType(200, Type = typeof(Review))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetReviewsByReviewer(int reviewerId)
    {
        if (!_reviewerRepository.Exists(reviewerId))
            return NotFound();
        
        var reviews = _mapper.Map<ICollection<ReviewDto>>(_reviewerRepository.GetReviewsByReviewerId(reviewerId));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(reviews);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateReviewer(int id, [FromBody] ReviewerDto? dto)
    {
        if (dto == null || !ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!_reviewerRepository.Exists(id))
            return NotFound();
        
        var reviewerMap = _mapper.Map<Reviewer>(dto);

        if (!_reviewerRepository.Update(id, reviewerMap))
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
    public IActionResult DeleteReviewer(int id)
    {
        if (!_reviewerRepository.Exists(id))
            return NotFound();

        if (!_reviewerRepository.DeleteById(id))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return BadRequest(ModelState);
        }
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return NoContent();
    } 
}