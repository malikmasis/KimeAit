using KimeAit.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KimeAit.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ExampleController : ControllerBase
{
    private readonly IKimeAitDbContext _dbContext;

    private readonly ILogger<ExampleController> _logger;

    public ExampleController(IKimeAitDbContext dbContext, ILogger<ExampleController> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string productName)
    {
        return Ok(await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == productName));
    }
}