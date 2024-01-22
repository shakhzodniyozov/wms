using WMS.Domain;

namespace WMS.Application;

public interface IImageService
{
    Task<Image> SetImages(IEntity entity, string base64);
    Task<Image> UpdateImage(string type, Guid id, string base64);
    void DeleteImage(IEntity entity, Guid id);
}
