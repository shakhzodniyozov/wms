using Microsoft.AspNetCore.Mvc;
using WMS.Application;

namespace WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EngineController : BaseController
{
    #region GET

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EngineDto>>> GetAll()
    {
        return Ok(await Mediator.Send(new GetAllEnginesQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EngineDto>> GetById(Guid id)
    {
        return Ok(await Mediator.Send(new GetEngineByIdQuery(id)));
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateEngineCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    #endregion

    #region PUT

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEngineCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    #endregion

    #region DELETE

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteEngineCommand(id));
        return NoContent();
    }

    #endregion
}