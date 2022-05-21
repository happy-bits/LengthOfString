using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace LengthOfString.Core;

public class CatCounter
{
    private readonly string _subscriptionKey;
    private readonly string _endpoint;
    private readonly string[] _catishWords;
    private readonly Log _log = Services.Log;

    public CatCounter(string subscriptionKey, string endpoint, string[] catishWords)
    {
        _subscriptionKey = subscriptionKey;
        _endpoint = endpoint;
        _catishWords = catishWords;
    }

    public int Count(Image image)
    {
        ComputerVisionClient client = Authenticate(_endpoint, _subscriptionKey);

        return AnalyzeImage(client, image).Result;
    }

    public bool IsGoodCat(Image image)
    {
        ComputerVisionClient client = Authenticate(_endpoint, _subscriptionKey);

        return IsGoodCat_Inner(client, image).Result;
    }

    private static ComputerVisionClient Authenticate(string endpoint, string key) =>

        new(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };

    private async Task<int> AnalyzeImage(ComputerVisionClient client, Image image)
    {
        List<VisualFeatureTypes?> features = new() { VisualFeatureTypes.Objects };

        var imageStream = ToStream(image, ImageFormat.Png);
        ImageAnalysis results = await client.AnalyzeImageInStreamAsync(imageStream, visualFeatures: features);

        var nrOfCats = 0;

        foreach (var x in results.Objects)
        {
            var prop = x.ObjectProperty.ToLower();

            _log.WriteLine($"{prop,-10}({x.Confidence})");

            if (_catishWords.Any(catish => prop.Contains(catish)))
            {
                nrOfCats++;
            }
        }

        return nrOfCats;
    }

    private static async Task<bool> IsGoodCat_Inner(ComputerVisionClient client, Image image)
    {
        List<VisualFeatureTypes?> features = new()
        {
            VisualFeatureTypes.Objects
        };

        var imageStream = ToStream(image, ImageFormat.Png);
        ImageAnalysis results = await client.AnalyzeImageInStreamAsync(imageStream, visualFeatures: features);
        imageStream.Close();

        var mostConfidentProperty = results.Objects.OrderByDescending(results => results.Confidence).FirstOrDefault();

        if (mostConfidentProperty == null) return false;

        return mostConfidentProperty.ObjectProperty.ToLower() == "cat";
    }

    private static Stream ToStream(Image image, ImageFormat format)
    {
        var stream = new MemoryStream();
        image.Save(stream, format);
        stream.Position = 0;
        return stream;
    }

}
