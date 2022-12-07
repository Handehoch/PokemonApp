using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Utils;

namespace PokemonApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController: Controller
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryController(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult CreateCountry([FromBody] CountryDto? dto)
    {
        if (dto == null)
            return BadRequest(ModelState);

        var country = _countryRepository
            .GetAll()
            .FirstOrDefault(c => string.Equals(c.Name.Trim(), dto.Name.Trim(),
                StringComparison.CurrentCultureIgnoreCase));

        if (country != null)
        {
            ModelState.AddModelError("", "Country already exists");
            return StatusCode(422, ModelState);
        }

        var countryMap = _mapper.Map<Country>(dto);
        if (!_countryRepository.Create(countryMap))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return StatusCode(500, ModelState);
        }

        return StatusCode(201, countryMap);
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Country>))]
    [ProducesResponseType(400)]
    public IActionResult GetCountries()
    {
        var countries =_mapper.Map<ICollection<CountryDto>>( _countryRepository.GetAll());

        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        return Ok(countries);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Country))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        if (!_countryRepository.Exists(id))
            return NotFound();
        
        var country = _mapper.Map<CountryDto>(_countryRepository.GetById(id));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(country);
    }

    [HttpGet("{countryId:int}/owners")]
    [ProducesResponseType(200, Type = typeof(ICollection<OwnerDto>))]
    [ProducesResponseType(400)]
    public IActionResult GetOwnersByCountryId(int countryId)
    {
        var owners = _mapper.Map<ICollection<OwnerDto>>(_countryRepository.GetOwnersByCountryId(countryId));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(owners);
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateCountry(int id, [FromBody] CountryDto? dto)
    {
        if (dto == null || !ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!_countryRepository.Exists(id))
            return NotFound();
        
        var countryMap = _mapper.Map<Country>(dto);

        if (!_countryRepository.Update(id, countryMap))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}