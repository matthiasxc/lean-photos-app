using leanPhotos.Logic.Interfaces;
using leanPhotos.Logic.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace leanPhotos.Logic.Services
{
    public class PhotoService : IPhotoService
    {
        private const string DEFAULT_PHOTO_URL = "https://jsonplaceholder.typicode.com/photos";
        private const string DEFAULT_QUERY_PARAM = "albumId";

        private JsonSerializerSettings _serializerSettings { get; set; }

        public PhotoService()
        {
            SourceUrl = DEFAULT_PHOTO_URL;
            QueryParameter = DEFAULT_QUERY_PARAM;
            _serializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
        }

        public string SourceUrl { get; private set; }
        public string QueryParameter { get; private set; }
        public string QueryString { get; private set; }

        public async Task<List<Photo>> GetAllPhotosAsync()
        {
            return await GetPhotosAsync(SourceUrl);
        }

        public async Task<List<Photo>> GetPhotosWithQueryAsync(string query)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add(QueryParameter, query);
            return await GetPhotosAsync(RenderQueryUrl(SourceUrl, parameters));
        }

        private async Task<List<Photo>> GetPhotosAsync(string sourceUrl)
        {
            using (var client = new HttpClient())
            {
                var photoContent = await client.GetStringAsync(sourceUrl);
                return JsonConvert.DeserializeObject<List<Photo>>(photoContent, _serializerSettings);
            }
        }

        private string RenderQueryUrl(string baseUrl, Dictionary<string, string> parameters)
        {
            string parameterString = "?";
            foreach (KeyValuePair<string, string> kvp in parameters)
            {
                parameterString += $"{kvp.Key}={kvp.Value}";
            }

            return baseUrl + parameterString;
        }
    }
}
