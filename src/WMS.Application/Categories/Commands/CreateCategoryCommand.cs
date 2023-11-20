using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public string Name { get; set; } = null!;
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IMapper mapper;
    private readonly ICategoryRepository categoryRepo;
    private readonly IUnitOfWork uow;

    public CreateCategoryCommandHandler(IMapper mapper, IUnitOfWork uow)
    {
        this.mapper = mapper;
        this.uow = uow;
        categoryRepo = uow.CategoryRepository;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = await categoryRepo.CreateAsync(mapper.Map<Category>(request));
        await uow.SaveChangesAsync(cancellationToken);

        return mapper.Map<CategoryDto>(category);
    }
}

