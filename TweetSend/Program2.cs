using Newtonsoft.Json;
using OAuth;
using System.Text;

internal class Program2
{
    // TODO - store these somewhere safe
    private const string APIKey = "AhbgeX6MnPN9CWzueX58BEVSr";
    private const string APISecret = "NEdzKiRWcrpmdXUyBhVoqUuAkRKqYFNg5E34qg2ZSt8yb5xA4q";
    private const string AccessToken = "6726397748-n8rzzDrmYh69rQ7Wmhj6rgSFY8E9ZTZN1WY5NZY";
    private const string AccessTokenSecret = "Wj5TWJq1AXTxM31jkuf2sYXQg3bF1Dp3E95Chq3zP9MUP";

    const string uploadMediaEndpoint = "https://upload.twitter.com/1.1/media/upload.json";
    const string createTweetEndpoint = "https://api.twitter.com/2/tweets";

    private static async Task Main(string[] args)
    {

        var mediaId = await UploadFileAsync();
        if (string.IsNullOrEmpty(mediaId))
        {
            Console.WriteLine("Failed to send media");
            return;
        }

        var tweetData = new
        {
            text = "Hello world",
            media = new
            {
                media_ids = new List<string> { mediaId }
            }
        };
        var oauth = new OAuthMessageHandler(APIKey, APISecret, AccessToken, AccessTokenSecret);

        var jsonData = JsonConvert.SerializeObject(tweetData);

        var createTweetRequest = new HttpRequestMessage(HttpMethod.Post, createTweetEndpoint)
        {
            Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
        };

        using var httpClient = new HttpClient(oauth);

        var response = await httpClient.SendAsync(createTweetRequest);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Tweet sent successfully!");
        }
        else
        {
            Console.WriteLine($"Failed to send tweet. Error: {response.ReasonPhrase}");
        }
    }

    private static async Task<string?> UploadFileAsync()
    {
        var oauth = new OAuthMessageHandler(APIKey, APISecret, AccessToken, AccessTokenSecret);

        var path = "image.jpg";
        var fileBytes = File.ReadAllBytes(path);
        var mediaType = "image/jpeg"; // Change this to match your file type

        var fileContent = new ByteArrayContent(fileBytes);
        fileContent.Headers.Add("Content-Type", mediaType);
        var multipartContent = new MultipartFormDataContent
        {
            { fileContent, "media" }
        };

        var createUploadRequest = new HttpRequestMessage(HttpMethod.Post, uploadMediaEndpoint)
        {
            Content = multipartContent
        };

        using var httpClient = new HttpClient(oauth);
        var uploadResponse = await httpClient.SendAsync(createUploadRequest);
        if (uploadResponse.IsSuccessStatusCode)
        {
            var responseContent = await uploadResponse.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<TwitterMediaUploadResponse>(responseContent);

            Console.WriteLine("Media uploaded successfully!");

            return deserializedResponse?.MediaId;

        }
        else
        {
            Console.WriteLine($"Failed to send tweet. Error: {uploadResponse.ReasonPhrase}");
        }

        return null;
    }
}
