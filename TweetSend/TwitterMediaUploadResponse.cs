using Newtonsoft.Json;

internal class TwitterMediaUploadResponse
{
    [JsonProperty("media_id")]
    public string MediaId { get; set; }
}
