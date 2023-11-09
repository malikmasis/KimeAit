using KimeAit.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KimeAit.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IKimeAitDbContext _dbContext;

    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IKimeAitDbContext dbContext, ILogger<ProductsController> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _dbContext
                        .Products
                        .SingleOrDefaultAsync(p => p.Id == id));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Get(string productName)
    {
        return Ok(await _dbContext
                        .Products
                        .Where(p => p.Name.ToLower().Contains(productName.ToLower()))
                        .ToListAsync());
    }
}