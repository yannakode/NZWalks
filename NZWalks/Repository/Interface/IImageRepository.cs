using NZWalks.Models.Domain;

namespace NZWalks.Repository.Interface
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
