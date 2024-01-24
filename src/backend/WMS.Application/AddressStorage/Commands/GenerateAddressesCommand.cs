using MediatR;

namespace WMS.Application;

public class GenerateAddressesCommand : IRequest
{
    public int Lines { get; set; }
    public int Sections { get; set; }
    public int Levels { get; set; }
    public int[] Cells { get; set; } = null!;
}

public class GenerateAddressesCommandHandler : IRequestHandler<GenerateAddressesCommand, Unit>
{
    private readonly IUnitOfWork uow;

    public GenerateAddressesCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<Unit> Handle(GenerateAddressesCommand request, CancellationToken cancellationToken)
    {
        for (int line = 1; line <= request.Lines; line++)
        {
            for (int section = 1; section <= request.Sections; section++)
            {
                for (int level = 1; level <= request.Levels; level++)
                {
                    for (int cell = 1; cell <= request.Cells[level - 1]; cell++)
                    {
                        await uow.AddressRepository.CreateAsync(new()
                        {
                            Line = line,
                            Section = section,
                            Level = level,
                            Cell = cell,
                            IsTopLevel = false
                        });
                    }
                }
            }
        }

        await uow.SaveChangesAsync();

        return Unit.Value;
    }
}
