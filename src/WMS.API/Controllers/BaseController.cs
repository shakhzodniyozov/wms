using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WMS.API;

public class BaseController : ControllerBase
{
    private IMediator mediator = null!;

    protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
