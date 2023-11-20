using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class GetCategoryByIdQuery : IRequest<CategoryDto>
{
    public GetCategoryByIdQuery(Guid id) => Id = id;

    public Guid Id { get; }
}

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public GetCategoryByIdQueryHandler(IUnitOfWork uow)
    {
        this.uow = uow;
        categoryRepo = uow.CategoryRepository;
    }

    private readonly IUnitOfWork uow;
    private readonly ICategoryRepository categoryRepo;

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepo.GetByIdAsync(request.Id);

        if (category == null)
            throw new Exception("Category with provided id was not found");

        return new CategoryDto(category.Id, category.Name);
    }
}