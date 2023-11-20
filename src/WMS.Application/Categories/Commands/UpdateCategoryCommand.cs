using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class UpdateCategoryCommand : IRequest<CategoryDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork uow;
    private readonly ICategoryRepository categoryRepo;

    public UpdateCategoryCommandHandler(IMapper mapper, IUnitOfWork uow)
    {
        this.mapper = mapper;
        this.uow = uow;
        categoryRepo = uow.CategoryRepository;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepo.GetByIdAsync(request.Id);

        if (category == null)
            throw new Exception("Category with provided id was not found.");

        mapper.Map(request, category);

        categoryRepo.Update(category);

        await uow.SaveChangesAsync(cancellationToken);

        return mapper.Map<CategoryDto>(category);
    }
}