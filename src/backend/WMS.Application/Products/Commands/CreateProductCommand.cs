using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class CreateProductCommand : IRequest
{
    public string Name { get; set; } = null!;
    public string? Code { get; set; }
    public decimal Price { get; set; }
    public bool IsEnabled { get; set; }
    public bool ForAllManufactors { get; set; }
    public bool ForAllModels { get; set; }
    public Guid? ManufacturerId { get; set; }
    public Guid CategoryId { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public List<ModelWithYearsOfIssueDto> Models { get; set; } = null!;
    public List<Guid> Engines { get; set; } = new();
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
{
    public CreateProductCommandHandler(IUnitOfWork uow, IMapper mapper, IImageService imageService)
    {
        this.uow = uow;
        productRepository = uow.ProductRepository;
        modelRepository = uow.ModelRepository;
        this.mapper = mapper;
        this.imageService = imageService;
    }

    private readonly IUnitOfWork uow;
    private readonly IProductRepository productRepository;
    private readonly IModelRepository modelRepository;
    private readonly IMapper mapper;
    private readonly IImageService imageService;

    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request);

        product.Prices.Add(new()
        {
            Value = request.Price,
            Product = product,
            DateTime = DateTime.UtcNow
        });

        if (request.ForAllModels)
        {
            var models = await modelRepository.GetAllAsync(x => x.ManufacturerId == request.ManufacturerId, disableTracking: true);
            models.ForEach(m => m.Products.Add(product));
        }
        else
        {
            foreach (var model in request.Models)
            {
                var models = await modelRepository.GetAllAsync(x => x.Name == model.Model && model.YearsOfIssue.Contains(x.YearOfIssue));
                product.Models.AddRange(models);
            }
        }

        var engines = await uow.EngineRepository.GetAllAsync(x => request.Engines.Contains(x.Id));

        product.Engines.AddRange(engines);
        product.EAN = await GenerateEAN();

        if (request.Image?.Length > 50)
        {
            if (request.Image != null)
                product.Image = await imageService.SetImages(product, request.Image);
        }

        await productRepository.CreateAsync(product);
        await uow.SaveChangesAsync();

        return Unit.Value;
    }

    private async Task<string> GenerateEAN()
    {
        var lastProduct = (await productRepository.GetAllAsync()).OrderBy(x => x.EAN).LastOrDefault();

        string lastProductNumber = "";

        if (lastProduct != null)
        {
            lastProductNumber = lastProduct.EAN.Substring(0, lastProduct.EAN.Length - 1);
            string nextProductNumber = (long.Parse(lastProductNumber.Substring(0, lastProductNumber.Length)) + 1).ToString();
            string ean = new string('0', 12 - nextProductNumber.Length) + nextProductNumber;
            CalculateCheckDigit(ref ean);
            return ean;
        }
        return "1000000000016";
    }

    private void CalculateCheckDigit(ref string ean)
    {
        int sum1 = 0;
        int sum2 = 0;
        char[] chars = ean.ToCharArray();
        Array.Reverse(chars);
        var reversedEan = new string(chars);

        for (int i = 0; i < reversedEan!.Length; i++)
        {
            if (i % 2 == 0)
                sum1 += (int)char.GetNumericValue(reversedEan[i]);
            else
                sum2 += (int)char.GetNumericValue(reversedEan[i]);
        }

        sum1 *= 3;

        ean += 10 - ((sum1 + sum2) % 10);
    }
}
