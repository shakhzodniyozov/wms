namespace WMS.Domain;

public class Employee : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName { get => $"{FirstName} {LastName}"; }
    public string? PhoneNumber { get; set; }
}
