using Microsoft.AspNetCore.Mvc;
using WMS.Application;

namespace WMS.API;

[ApiController]
[Route("api/[controller]")]
public class ModelsController : BaseController
{

    #region Manufacturer

    [HttpPost("manufacturer")]
    public async Task<ActionResult<ManufacturerDto>> CreateManufacturer([FromBody] CreateManufacturerCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("manufacturer")]
    public async Task<ActionResult<ManufacturerDto>> UpdateManufacturer(UpdateManufacturerCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("manufacturer/{id}")]
    public async Task<IActionResult> DeleteManufacturer(Guid id)
    {
        await Mediator.Send(new DeleteManufacturerCommand(id));

        return Ok();
    }

    [HttpGet("manufacturers-with-models")]
    public async Task<ActionResult<IEnumerable<ManufacturerWithModelsDto>>> GetAllManufacturers()
    {
        return Ok(await Mediator.Send(new GetManufacturersWithModelsQuery()));
    }

    [HttpGet("withYears/{manufacturerId}")]
    public async Task<ActionResult<List<ModelWithYearsOfIssueDto>>> GetModelsWithYearsOfIssue(Guid manufacturerId)
    {
        return Ok(await Mediator.Send(new GetModelsWithYearsOfIssue(manufacturerId)));
    }

    #endregion

    #region Models

    [HttpPost]
    public async Task<ActionResult<ModelDto>> Create(CreateModelCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<ActionResult<ModelDto>> Update(UpdateModelCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ModelDto>> GetModelById(Guid id)
    {
        return Ok(await Mediator.Send(new GetModelByIdQuery(id)));
    }

    #endregion
}