using Newtonsoft.Json;

namespace leanPhotos.Logic.Models
{
    public sealed class Photo
    {
        [JsonProperty("albumId")]
        public int AlbumId { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }
}
