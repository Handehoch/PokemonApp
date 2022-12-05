using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

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
}
