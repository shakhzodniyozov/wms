namespace WMS.Application;

public class SupplyOfGoodsDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public List<SupplyDetailsDto> SupplyDetails { get; set; } = new();
}
