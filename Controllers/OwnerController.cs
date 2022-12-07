using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Utils;

namespace PokemonApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnerController: Controller
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public OwnerController(IOwnerRepository ownerRepository, ICountryRepository countryRepository, IMapper mapper)
    {
        _ownerRepository = ownerRepository;
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Owner))]
    [ProducesResponseType(400)]
    public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto? dto)
    {
        if (dto == null)
            return BadRequest(ModelState);

        var owner = _ownerRepository.GetAll()
            .FirstOrDefault(o => string.Equals(o.LastName.Trim(), dto.LastName.Trim(), 
                StringComparison.CurrentCultureIgnoreCase));

        if (owner != null)
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("OWNER_EXISTS") ?? string.Empty);
            return StatusCode(422, ModelState);
        }

        var ownerMap = _mapper.Map<Owner>(dto);
        ownerMap.Country = _countryRepository.GetById(countryId);

        if (!_ownerRepository.Create(ownerMap))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return StatusCode(500, ModelState);
        }

        return StatusCode(201, ownerMap);
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
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateOwner(int id, [FromBody] OwnerDto? dto)
    {
        if (dto == null || !ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_ownerRepository.Exists(id))
            return NotFound();
        
        var ownerMap = _mapper.Map<Owner>(dto);

        if (!_ownerRepository.Update(id, ownerMap))
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
    public IActionResult DeleteOwner(int id)
    {
        if (!_ownerRepository.Exists(id))
            return NotFound();

        if (!_ownerRepository.DeleteById(id))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return BadRequest(ModelState);
        }
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return NoContent();
    } 
}