using MediatR;
using Microsoft.AspNetCore.Mvc;
using WMS.Application;

namespace WMS.API;

[ApiController]
[Route("api/category")]
public class CategoryController : BaseController
{
    private IMediator mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
    {
        return await mediator.Send(new GetCategoryByIdQuery(id));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        return Ok(await mediator.Send(new GetAllCategoriesQuery()));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpPut]
    public async Task<ActionResult<CategoryDto>> Update([FromBody] UpdateCategoryCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await mediator.Send(new DeleteCategoryCommand(id));
        return Ok();
    }
}