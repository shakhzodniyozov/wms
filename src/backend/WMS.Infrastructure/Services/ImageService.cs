using Microsoft.AspNetCore.Hosting;
using WMS.Application;
using WMS.Domain;

namespace WMS.Infrastructure;

public class ImageService : IImageService
{
    public ImageService(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    private readonly IWebHostEnvironment environment;
    public string ImagePath { get { return environment.WebRootPath; } }

    public void DeleteImage(IEntity entity, Guid id)
    {
        var directory = new DirectoryInfo($"{ImagePath}/images/{entity.GetType().Name}/{id}");
        if (directory.Exists)
            directory.Delete(true);
    }

    public async Task<Image> SetImages(IEntity entity, string base64)
    {
        string type = entity.GetType().Name;

        string fileExtension = ParseFileExtension(base64);
        string fileName = entity.Id.ToString();
        Directory.CreateDirectory($"{ImagePath}/images/{type}/{entity.Id}");

        byte[] imageBytes = Convert.FromBase64String(base64.Substring(base64.IndexOf(',') + 1));

        using (FileStream stream = new FileStream($"{ImagePath}/images/{type}/{entity.Id}/{fileName}.{fileExtension}", FileMode.Create))
        {
            await stream.WriteAsync(imageBytes, 0, imageBytes.Count());
        }

        return new Image() { Name = $"{fileName}.{fileExtension}", ProductId = entity.Id };
    }

    private string ParseFileExtension(string base64)
    {
        string head = base64.Split(';')[0];
        string extension = head.Substring(base64.IndexOf('/') + 1);

        return extension;
    }

    public async Task<Image> UpdateImage(string type, Guid id, string base64)
    {
        var directory = new DirectoryInfo($"{ImagePath}/images/{type}/{id}");

        if (directory.Exists)
            directory.GetFiles().FirstOrDefault()?.Delete();
        else
            directory.Create();

        string fileExtension = ParseFileExtension(base64!);
        string fileName = id.ToString();
        byte[] imageBytes = Convert.FromBase64String(base64!.Substring(base64.IndexOf(',') + 1));

        using (FileStream stream = new FileStream($"{ImagePath}/images/{type}/{id}/{fileName}.{fileExtension}", FileMode.Create))
        {
            await stream.WriteAsync(imageBytes, 0, imageBytes.Count());
        }

        return new Image() { Name = $"{fileName}.{fileExtension}" };
    }
}
