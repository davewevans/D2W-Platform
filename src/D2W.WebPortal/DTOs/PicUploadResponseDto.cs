using System.Text.Json.Serialization;

namespace D2W.WebPortal.DTOs
{
    public class PicUploadResponseDto
    {
        [JsonPropertyName("fileURL")]
        public string FileURL { get; set; }
    }
}
