using MediatR;

namespace WMS.Application;

public class DeleteProductCommand : IRequest
{
    public DeleteProductCommand(Guid productId) => ProductId = productId;

    public Guid ProductId { get; }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    public DeleteProductCommandHandler(IUnitOfWork uow, IImageService imageService)
    {
        this.uow = uow;
        this.imageService = imageService;
    }

    private readonly IUnitOfWork uow;
    private readonly IImageService imageService;

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await uow.ProductRepository.GetByIdAsync(request.ProductId);

        if (product == null)
            throw new Exception("Product with provided Id was not found.");

        uow.ProductRepository.Delete(product);
        imageService.DeleteImage(product, product.Id);
        await uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

