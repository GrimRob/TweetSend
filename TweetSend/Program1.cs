using Newtonsoft.Json;
using OAuth;
using System.Text;

internal class Program1
{
    // TODO - store these somewhere safe
    private const string APIKey = "AhbgeX6MnPN9CWzueX58BEVSr";
    private const string APISecret = "NEdzKiRWcrpmdXUyBhVoqUuAkRKqYFNg5E34qg2ZSt8yb5xA4q";
    private const string AccessToken = "6726397748-n8rzzDrmYh69rQ7Wmhj6rgSFY8E9ZTZN1WY5NZY";
    private const string AccessTokenSecret = "Wj5TWJq1AXTxM31jkuf2sYXQg3bF1Dp3E95Chq3zP9MUP";

    const string createTweetEndpoint = "https://api.twitter.com/2/tweets";

    private static async Task Main(string[] args)
    {
        var tweet = "Hello world";
        var oauth = new OAuthMessageHandler(APIKey, APISecret, AccessToken, AccessTokenSecret);

        var tweetData = new { text = tweet };
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
}