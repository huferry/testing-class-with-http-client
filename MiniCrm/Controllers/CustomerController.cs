using Microsoft.AspNetCore.Mvc;
using MiniCrm.Models;
using MiniCrm.Services;

namespace MiniCrm.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly Customer[] _data = {
        new() { Id = "A", Name = "John"},
        new() { Id = "B", Name = "Jack"},
        new() { Id = "C", Name = "Mark"},
        new() { Id = "D", Name = "Luke"},
    };

    private readonly ZipCodeService _zipCodeService;

    public CustomerController(ZipCodeService zipCodeService)
    {
        _zipCodeService = zipCodeService;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCustomer(string customerId)
    {
        var customer = _data
            .SingleOrDefault(c => c.Id.Equals(customerId, StringComparison.InvariantCultureIgnoreCase));

        if (customer is null) return NotFound();
        
        customer.Zipcode = await _zipCodeService.GetZipCode(customerId);
        return Ok(customer);
    }
}