using AutoMapper;
using MediatR;

namespace WMS.Application;

public class GetModelsWithYearsOfIssue : IRequest<List<ModelWithYearsOfIssueDto>>
{
    public GetModelsWithYearsOfIssue(Guid id) => ManufacturerId = id;

    public Guid ManufacturerId { get; set; }
}

public class GetModelsWithYearsOfIssueHandler : IRequestHandler<GetModelsWithYearsOfIssue, List<ModelWithYearsOfIssueDto>>
{
    private readonly IUnitOfWork uow;
    private readonly IModelRepository modelRepository;

    public GetModelsWithYearsOfIssueHandler(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        modelRepository = uow.ModelRepository;
    }

    public async Task<List<ModelWithYearsOfIssueDto>> Handle(GetModelsWithYearsOfIssue request, CancellationToken cancellationToken)
    {
        var models = await modelRepository.GetAllAsync(x => x.ManufacturerId == request.ManufacturerId, disableTracking: true);

        var dtos = models.GroupBy(s => s.Name).Select(group =>
        {
            var dto = new ModelWithYearsOfIssueDto()
            {
                Model = group.Key
            };

            foreach (var model in group)
                dto.YearsOfIssue.Add(model.YearOfIssue);

            return dto;
        }).ToList();

        return dtos;
    }
}