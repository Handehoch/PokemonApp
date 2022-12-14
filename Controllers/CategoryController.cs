using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Utils;

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

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult CreateCategory([FromBody] CategoryDto? dto)
    {
        if (dto == null)
            return BadRequest(ModelState);

        var category = _categoryRepository
            .GetAll()
            .FirstOrDefault(c => string.Equals(c.Name.Trim(), dto.Name.Trim(),
                StringComparison.CurrentCultureIgnoreCase));

        if (category != null)
        {
            ModelState.AddModelError("", "Category already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categoryMap = _mapper.Map<Category>(dto);

        if (!_categoryRepository.Create(categoryMap))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return StatusCode(500, ModelState);
        }

        return StatusCode(201, categoryMap);
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(ICollection<Category>))]
    [ProducesResponseType(400)]
    public IActionResult GetCategories()
    {
        var categories =_mapper.Map<ICollection<CategoryDto>>( _categoryRepository.GetAll());
        return !ModelState.IsValid ? BadRequest(ModelState) : Ok(categories);
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

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateCategory(int id, [FromBody] CategoryDto? dto)
    {
        if (dto == null || !ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!_categoryRepository.Exists(id))
            return NotFound();
        
        var categoryMap = _mapper.Map<Category>(dto);

        if (!_categoryRepository.Update(id, categoryMap))
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
    public IActionResult DeleteCategory(int id)
    {
        if (!_categoryRepository.Exists(id))
            return NotFound();

        if (!_categoryRepository.DeleteById(id))
        {
            ModelState.AddModelError("", ErrorMessage.Errors.Get("SAVE_ERROR") ?? string.Empty);
            return BadRequest(ModelState);
        }
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return NoContent();
    } 
}