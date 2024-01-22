using MediatR;

namespace WMS.Application;

public class UpdateProductCommand : CreateProductCommand, IRequest
{
    public Guid Id { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    public UpdateProductCommandHandler(IUnitOfWork uow, IImageService imageService)
    {
        this.uow = uow;
        this.imageService = imageService;
        productRepository = uow.ProductRepository;
        modelRepository = uow.ModelRepository;
    }

    private readonly IUnitOfWork uow;
    private readonly IImageService imageService;
    private readonly IProductRepository productRepository;
    private readonly IModelRepository modelRepository;

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, "Image,Models");

        if (product == null)
            throw new Exception("Product with provided Id was not found.");

        product.Name = request.Name;
        product.Code = request.Code;
        product.CategoryId = request.CategoryId;
        product.Description = request.Description;

        if (product.LastPrice != request.Price)
        {
            product.Prices.Add(new()
            {
                Value = request.Price,
                DateTime = DateTime.UtcNow,
                Product = product
            });
        }

        if (!string.IsNullOrEmpty(request.Image) && request.Image != product.Image?.Name)
            product.Image = await imageService.UpdateImage(product.GetType().Name, product.Id, request.Image);

        product.Models.Clear();

        if (!request.ForAllManufactors && request.ForAllModels)
        {
            var models = await uow.ModelRepository.GetAllAsync(x => x.ManufacturerId == request.ManufacturerId);
            product.Models.AddRange(models);
        }
        else
        {
            foreach (var model in request.Models)
            {
                var models = await modelRepository.GetAllAsync(x => x.Name == model.Model && model.YearsOfIssue.Contains(x.YearOfIssue));
                product.Models.AddRange(models);
            }
        }

        productRepository.Update(product);
        await uow.SaveChangesAsync();

        return Unit.Value;
    }
}