using AutoMapper;
using MediatR;

namespace WMS.Application;

public class DeleteCategoryCommand : IRequest
{
    public DeleteCategoryCommand(Guid id) => Id = id;

    public Guid Id { get; }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly IMapper mapper;
    private readonly ICategoryRepository categoryRepo;
    private readonly IUnitOfWork uow;

    public DeleteCategoryCommandHandler(IMapper mapper, IUnitOfWork uow)
    {
        this.mapper = mapper;
        this.uow = uow;
        categoryRepo = uow.CategoryRepository;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepo.GetByIdAsync(request.Id);

        if (category == null)
            throw new Exception("Category with provided Id was not found.");

        categoryRepo.Delete(category);

        await uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

