using Microsoft.AspNetCore.Mvc;
using WMS.Application;

namespace WMS.API;

[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseController
{
    #region GET

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        return Ok(await Mediator.Send(new GetProductsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetailsDto>> GetById(Guid id)
    {
        return Ok(await Mediator.Send(new GetProductByIdQuery(id)));
    }

    [HttpGet("new/preliminary")]
    public async Task<ActionResult<ManufacturersAndCategoriesDto>> GetPreliminaryDataForNewProduct()
    {
        return Ok(await Mediator.Send(new GetPreliminaryDataForNewProduct()));
    }

    [HttpGet("suggestion")]
    public async Task<ActionResult<IEnumerable<ProductForAutocompleteDto>>> GetSuggestions([FromQuery] string productName)
    {
        return Ok(await Mediator.Send(new GetSuggestionQuery(productName)));
    }

    [HttpGet("getById")]
    public async Task<ActionResult> GetProductByEAN([FromQuery] string ean)
    {
        return Ok(await Mediator.Send(new GetProductByEANQuery(ean)));
    }

    #endregion

    #region POST

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    #endregion

    #region DELETE

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(Guid productId)
    {
        await Mediator.Send(new DeleteProductCommand(productId));
        return NoContent();
    }

    #endregion

    #region UPDATE

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    #endregion
}
