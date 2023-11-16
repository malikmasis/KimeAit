using KimeAit.Api.Data;
using KimeAit.Api.Entities;
using KimeAit.Api.Models;
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
                        .Include(p => p.AlternativeProducts)
                        .SingleOrDefaultAsync(p => p.Id == id && p.IsApproved));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Get(string productName)
    {
        return Ok(await _dbContext
                        .Products
                        .Include(p => p.AlternativeProducts)
                        .Where(p => p.Name.ToLower().Contains(productName.ToLower()) && p.IsApproved)
                        .ToListAsync());
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct(
        [FromBody] ProductRequestModel product,
        CancellationToken cancellationToken)
    {
        var createdProduct = new Product
        {
            Origin = product.Origin, Desc = product.Desc, Name = product.Name, IsHaram = product.IsHaram
        };

        _dbContext
            .Products
            .Add(createdProduct);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, createdProduct.Id);
    }
}