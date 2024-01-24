using Microsoft.AspNetCore.Mvc;
using WMS.Application;

namespace WMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StorageController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressDto>>> GetAll()
    {
        return Ok(await Mediator.Send(new GetAddressesQuery()));
    }

    [HttpGet("{addressId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsInCell(Guid addressId)
    {
        return Ok(await Mediator.Send(new GetProductsInCellQuery(addressId)));
    }

    [HttpPost("generate-addresses")]
    public async Task<IActionResult> GenerateAddresses([FromBody] GenerateAddressesCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}