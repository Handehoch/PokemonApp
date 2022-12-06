using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnerController: Controller
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;

    public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
    {
        _ownerRepository = ownerRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Owner>))]
    [ProducesResponseType(400)]
    public IActionResult GetOwners()
    {
        var owners =_mapper.Map<ICollection<OwnerDto>>( _ownerRepository.GetAll());

        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        return Ok(owners);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Owner))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        if (!_ownerRepository.Exists(id))
            return NotFound();
        
        var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetById(id));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(owner);
    }

    [HttpGet("{ownerId:int}/pokemon")]
    [ProducesResponseType(200, Type = typeof(Owner))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetOwnerPokemons(int ownerId)
    {
        if (!_ownerRepository.Exists(ownerId))
            return NotFound();
        
        var pokemons = _mapper.Map<ICollection<PokemonDto>>(_ownerRepository.GetPokemonsByOwnerId(ownerId));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(pokemons);
    }
}