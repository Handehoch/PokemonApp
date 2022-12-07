using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Utils;

namespace PokemonApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : Controller
{
    private readonly IPokemonRepository _pokemonRepository;
    private readonly IMapper _mapper;

    public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
    {
        _pokemonRepository = pokemonRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Pokemon))]
    [ProducesResponseType(400)]
    public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto? dto)
    {
        if (dto == null)
            return BadRequest(ModelState);

        var pokemon = _pokemonRepository.GetAll()
            .FirstOrDefault(p => string.Equals(p.Name.Trim(), dto.Name.Trim(),
                StringComparison.CurrentCultureIgnoreCase));

        if (pokemon != null)
        {
            ModelState.AddModelError("", "Pokemon already exists");
            return StatusCode(422, ModelState);
        }

        var pokemonMap = _mapper.Map<Pokemon>(dto);
        if (!_pokemonRepository.CreateWithRelations(ownerId, categoryId, pokemonMap))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return StatusCode(500, ModelState);
        }

        return StatusCode(201, pokemonMap);
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemons()
    {
        var pokemons =_mapper.Map<ICollection<PokemonDto>>( _pokemonRepository.GetAll());

        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        return Ok(pokemons);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Pokemon))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        if (!_pokemonRepository.Exists(id))
            return NotFound();
        
        var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetById(id));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(pokemon);
    }

    [HttpGet("{id:int}/rating")]
    [ProducesResponseType(200, Type = typeof(double))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetRating(int id)
    {
        if (!_pokemonRepository.Exists(id))
            return NotFound();

        var rating = _pokemonRepository.GetRatingById(id);
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(rating);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdatePokemon(int id, [FromBody] PokemonDto? dto)
    {
        if (dto == null || !ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!_pokemonRepository.Exists(id))
            return NotFound();
        
        var pokemonMap = _mapper.Map<Pokemon>(dto);

        if (!_pokemonRepository.Update(id, pokemonMap))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}
