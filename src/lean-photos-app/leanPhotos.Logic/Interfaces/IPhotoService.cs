using leanPhotos.Logic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace leanPhotos.Logic.Interfaces
{
    public interface IPhotoService
    {
        string SourceUrl { get; }
        string QueryParameter { get; }
        string QueryString { get; }

        Task<List<Photo>> GetAllPhotosAsync();
        Task<List<Photo>> GetPhotosWithQueryAsync(string query);
    }
}
