using Microsoft.AspNetCore.Mvc;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : Controller
{
    private readonly IRepository<Pokemon> _pokemonRepository;

    public PokemonController(IRepository<Pokemon> pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
    public IActionResult GetPokemons()
    {
        var pokemons = this._pokemonRepository.GetAll();

        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        return Ok(pokemons);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Pokemon))]
    public IActionResult GetById(int id)
    {
        var pokemon = this._pokemonRepository.GetById(id);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(pokemon);
    }
}
