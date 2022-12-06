using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

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

    // TODO implement GetOwnersByCountryId
    [HttpGet("{countryId:int}/owners")]
    [ProducesResponseType(200, Type = typeof(ICollection<OwnerDto>))]
    [ProducesResponseType(400)]
    IActionResult GetOwnersByCountryId()
    {
        
    }
    
}