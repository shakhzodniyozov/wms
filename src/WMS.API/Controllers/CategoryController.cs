using MediatR;
using Microsoft.AspNetCore.Mvc;
using WMS.Application;
using WMS.Domain;

namespace WMS.API;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private IMediator mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
    {
        return await mediator.Send(new GetCategoryByIdQuery(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}
