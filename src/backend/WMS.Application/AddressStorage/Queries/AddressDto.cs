namespace WMS.Application;

public class AddressDto
{
    public Guid Id { get; set; }
    public int Line { get; set; }
    public int Section { get; set; }
    public int Level { get; set; }
    public int Cell { get; set; }
}
