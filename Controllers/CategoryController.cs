using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController: Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Category>))]
    [ProducesResponseType(400)]
    public IActionResult GetCategories()
    {
        var categories =_mapper.Map<ICollection<CategoryDto>>( _categoryRepository.GetAll());

        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(Category))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        if (!_categoryRepository.Exists(id))
            return NotFound();
        
        var category = _mapper.Map<CategoryDto>(_categoryRepository.GetById(id));
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(category);
    }

    [HttpGet("pokemon/{categoryId:int}")]
    [ProducesResponseType(200, Type = typeof(Category))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult GetPokemonsByCategoryId(int categoryId)
    {
        if (!_categoryRepository.Exists(categoryId))
            return NotFound();

        var pokemons = _mapper.Map<ICollection<PokemonDto>>(
            _categoryRepository.GetPokemonByCategoryId(categoryId));

        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(pokemons);
    }
}