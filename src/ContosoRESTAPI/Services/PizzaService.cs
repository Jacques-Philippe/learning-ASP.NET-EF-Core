using ContosoRESTAPI.Data;
using ContosoRESTAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoRESTAPI.Services;

public class PizzaService
{
    private readonly PizzaContext _context;

    //When the PizzaService instance is created, a PizzaContext will be injected as a dependency
    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        //AsNoTracking instructs EF Core to disable change tracking, to optimize performance.
        return _context.Pizzas.AsNoTracking().ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
            .Include(p => p.Toppings) //include Toppings field
            .Include(p => p.Sauce) //include Sauce field
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id); //for given id return default or the pizza
    }

    public Pizza? Create(Pizza newPizza)
    {
        // add the new pizza to the EF Core's object graph
        _context.Pizzas.Add(newPizza);
        // instruct EF core to persist the object changes to the db
        _context.SaveChanges();

        return newPizza;
    }

    public void DeleteById(int id)
    {
        var pizzaToDelete = _context.Pizzas.Find(id);
        if (pizzaToDelete is null)
        {
            throw new NullReferenceException("Pizza does not exist 😭");
        }
        else
        {
            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
        }
    }

    public void AddTopping(int pizzaId, int toppingId)
    {
        throw new NotImplementedException();
    }

    public void UpdateSauce(int pizzaId, int sauceId)
    {
        // ensure pizza and sauce both exist
        var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
        var sauceToUpdate = _context.Sauces.Find(sauceId);
        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new NullReferenceException("Pizza or sauce does not exist 😭");
        }
        // update pizza's sauce
        pizzaToUpdate.Sauce = sauceToUpdate;
        // instruct EF core to persist the object changes to the db
        _context.SaveChanges();
    }
}
