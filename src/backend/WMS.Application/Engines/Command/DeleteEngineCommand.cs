using MediatR;

namespace WMS.Application;

public class DeleteEngineCommand : IRequest
{
    public DeleteEngineCommand(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteEngineCommandHandler : IRequestHandler<DeleteEngineCommand, Unit>
{
    private readonly IUnitOfWork uow;
    private readonly IEngineRepository engineRepository;

    public DeleteEngineCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
        engineRepository = uow.EngineRepository;
    }

    public async Task<Unit> Handle(DeleteEngineCommand request, CancellationToken cancellationToken)
    {
        var engine = await engineRepository.GetByIdAsync(request.Id);

        if (engine == null)
            throw new Exception("Engine with provided Id was not found.");

        engineRepository.Delete(engine);
        await uow.SaveChangesAsync();

        return Unit.Value;
    }
}
