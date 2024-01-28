using Microsoft.AspNetCore.Mvc;
using WMS.Application;
using WMS.Domain;

namespace WMS.API;

[ApiController]
[Route("api/[controller]")]
public class SupplyController : BaseController
{
    #region GET

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplyOfGoodsDto>>> GetAll()
    {
        return Ok(await Mediator.Send(new GetAllSuppliesQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplyOfGoods>> GetById(Guid id)
    {
        return Ok(await Mediator.Send(new GetSupplyByIdQuery(id)));
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSupplyOfGoodsCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    #endregion

    #region PUT

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSupplyCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    #endregion

    #region DELETE

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteSupplyOfGoodsCommand(id));
        return NoContent();
    }

    #endregion
}
