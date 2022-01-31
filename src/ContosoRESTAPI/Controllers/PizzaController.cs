using Microsoft.AspNetCore.Mvc;
using ContosoRESTAPI.Services;
using ContosoRESTAPI.Models;

namespace ContosoRESTAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    PizzaService _PizzaService;

    public PizzaController(PizzaService service)
    {
        _PizzaService = service;
    }

    [HttpGet]
    public IEnumerable<Pizza> GetAll()
    {
        return _PizzaService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Pizza> GetById(int id)
    {
        var pizza = _PizzaService.GetById(id);
        if (pizza is not null)
            return pizza;
        return NotFound();
    }

    [HttpPost]
    public IActionResult Create(Pizza newPizza)
    {
        var pizza = _PizzaService.Create(newPizza);
        return CreatedAtAction(nameof(GetById), new { Id = pizza.Id }, pizza);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var pizza = _PizzaService.GetById(id);
        if (pizza is not null)
        {
            _PizzaService.DeleteById(id);
            return Ok();
        }
        return NotFound();
    }

    [HttpPut("{id}/add-topping")]
    public IActionResult AddTopping(int pizzaId, int toppingId)
    {
        var pizza = _PizzaService.GetById(pizzaId);
        if (pizza is not null)
        {
            //TODO check topping exists?
            _PizzaService.AddTopping(pizzaId, toppingId);
            return NoContent();
        }
        return BadRequest();
    }

    [HttpPut("{id}/update-sauce")]
    public IActionResult UpdateSauce(int pizzaId, int sauceId)
    {
        var pizza = _PizzaService.GetById(pizzaId);
        if (pizza is not null)
        {
            _PizzaService.UpdateSauce(pizzaId, sauceId);
            return NoContent();
        }
        return BadRequest();
    }
}
