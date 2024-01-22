using AutoMapper;
using MediatR;
using WMS.Domain;

namespace WMS.Application;

public class GetManufacturersWithModelsQuery : IRequest<IEnumerable<ManufacturerWithModelsDto>>
{

}

public class GetManufacturersWithModelsQueryHandler : IRequestHandler<GetManufacturersWithModelsQuery, IEnumerable<ManufacturerWithModelsDto>>
{
    private IUnitOfWork uow;
    private IMapper mappper;

    public GetManufacturersWithModelsQueryHandler(IMapper mapper, IUnitOfWork uow)
    {
        this.uow = uow;
        this.mappper = mapper;
    }

    public async Task<IEnumerable<ManufacturerWithModelsDto>> Handle(GetManufacturersWithModelsQuery request, CancellationToken cancellationToken)
    {
        var manufacturers = await uow.ManufacturerRepository.GetAllAsync(includeProperties: "Models", disableTracking: true);
        var dtos = manufacturers.Select(x =>
        {
            return new ManufacturerWithModelsDto()
            {
                Manufacturer = x.Name!,
                Models = x.Models.OrderBy(x => x.Name)
                                .GroupBy(m => m.Name)
                                .Select(k => new ModelWithYears()
                                {
                                    ModelName = k.Key,
                                    YearsOfIssue = GetCombinedYearOfIssue(k.Select(y => y.YearOfIssue).ToList())
                                })
                                .OrderBy(x => x.ModelName)
            };
        });

        return dtos;
    }

    private string GetCombinedYearOfIssue(List<int> years)
    {
        years = years.Order().ToList();

        if (years.Count == 1)
            return years[0].ToString();

        string result = "";
        int head = years[0];
        int tail = 0;

        for (int i = 1; i < years.Count; i++)
        {
            if (years[i] - years[i - 1] == 1)
                tail = years[i];
            else if (head < tail)
            {
                result += $"{head}-{tail}, ";
                head = years[i];
            }
            else
                result += $"{head}, ";
        }

        result += $"{head}-{tail}";
        return result;
    }
}