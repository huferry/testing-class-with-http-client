using Microsoft.AspNetCore.Mvc;
using MiniCrm.Models;
using MiniCrm.Services;

namespace MiniCrm.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private Customer[] Data => new []
    {
        new Customer { Id = 1, Name = "John"},
        new Customer { Id = 2, Name = "Jack"},
        new Customer { Id = 3, Name = "Mark"},
        new Customer { Id = 4, Name = "Luke"},
    };

    private readonly ZipCodeService _zipCodeService;

    public CustomerController(ZipCodeService zipCodeService)
    {
        _zipCodeService = zipCodeService;
    }

    [HttpGet]
    public async IAsyncEnumerable<Customer> GetAllCustomers()
    {
        foreach (var customer in Data)
        {
            customer.Zipcode = await _zipCodeService.GetZipCode(customer.Id);
            yield return customer;
        }
    }
    
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomer(int customerId)
    {
        var customer = Data
            .SingleOrDefault(c => c.Id == customerId);

        if (customer is null) return NotFound();
        
        customer.Zipcode = await _zipCodeService.GetZipCode(customerId);
        return Ok(customer);
    }
}