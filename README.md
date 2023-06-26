# TweetSend

This is C# example of how to send a Tweet with the [Twitter API](https://api.twitter.com/2/tweets).

The first example does not use media.

# Preconditions

You need to login in to Twitter and create a Project and App at the [Google Developer Dashboard](https://developer.twitter.com/en/portal/dashboard).

Once that's set up (you may need to keep trying) then you need to generate an Api Key and Secret. You also need a writeable Access Token and Secret (you don't need a Bearer Token for this). The make a writeable token you will need to edit your `User authentication settings` (even though there is no user) and choose `Read and write` for your `App permissions`.

You should have four strings which look something like this:

```
private const string APIKey = "AhbgeX6MnPN9CWzueX58BEVSr";
private const string APISecret = "NEdzKiRWcrpmdXUyBhVoqUuAkRKqYFNg5E34qg2ZSt8yb5xA4q";
private const string AccessToken = "6726397748-n8rzzDrmYh69rQ7Wmhj6rgSFY8E9ZTZN1WY5NZY";
private const string AccessTokenSecret = "Wj5TWJq1AXTxM31jkuf2sYXQg3bF1Dp3E95Chq3zP9MUP";
```

Replace my example (not a real set of keys) with your own and try the examples.

# Examples

1. [No media](TweetSend/Program1.cs).