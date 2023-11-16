using Microsoft.Extensions.DependencyInjection;
using WMS.Domain;

namespace WMS.Infrastructure;

public class DbInitializer
{
    public static void Init(IServiceProvider services)
    {
        var scope = services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        AddCategories(ctx);
        AddManufacturerAndModels(ctx);
        ctx.SaveChanges();
    }

    private static void AddCategories(ApplicationDbContext ctx)
    {
        var categories = new List<Category>()
        {
            new() {Name = "Кузовная часть"},
            new() {Name = "Тормозная система"},
            new() {Name = "Двигатель"},
            new() {Name = "Ходовая часть"},
            new() {Name = "Интерьер"},
            new() {Name = "Шины и диски"}
        };

        ctx.AddRange(categories);
    }

    private static void AddManufacturerAndModels(ApplicationDbContext ctx)
    {
        var mercedes = new Manufacturer() { Name = "Mercedes-Benz" };
        var opel = new Manufacturer() { Name = "Opel" };

        mercedes.Models.AddRange(new Model[]
        {
            new() {ModelName = "C Class", YearOfIssue = 1995, BodyType = BodyTypes.Sedan},
            new() {ModelName = "C Class", YearOfIssue = 1996, BodyType = BodyTypes.Sedan},
            new() {ModelName = "C Class", YearOfIssue = 1997, BodyType = BodyTypes.Sedan},
            new() {ModelName = "C Class", YearOfIssue = 1998, BodyType = BodyTypes.Sedan},
            new() {ModelName = "C Class", YearOfIssue = 1999, BodyType = BodyTypes.Sedan},
            new() {ModelName = "C Class", YearOfIssue = 2000, BodyType = BodyTypes.Sedan},
            new() {ModelName = "E Class", YearOfIssue = 1995, BodyType = BodyTypes.Sedan},
            new() {ModelName = "E Class", YearOfIssue = 1996, BodyType = BodyTypes.Sedan},
            new() {ModelName = "E Class", YearOfIssue = 1997, BodyType = BodyTypes.Sedan},
            new() {ModelName = "E Class", YearOfIssue = 1998, BodyType = BodyTypes.Sedan},
            new() {ModelName = "E Class", YearOfIssue = 1999, BodyType = BodyTypes.Sedan},
            new() {ModelName = "E Class", YearOfIssue = 2000, BodyType = BodyTypes.Sedan}
        });

        opel.Models.AddRange(new Model[]
        {
            new() {ModelName = "Astra F", YearOfIssue = 1991, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 1992, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 1993, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 1994, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 1995, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 1996, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 1997, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 1998, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 1999, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 2000, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 2002, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 2003, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 2004, BodyType = BodyTypes.Universal},
            new() {ModelName = "Astra F", YearOfIssue = 2005, BodyType = BodyTypes.Universal}
        });

        ctx.AddRange(mercedes, opel);
    }
}
