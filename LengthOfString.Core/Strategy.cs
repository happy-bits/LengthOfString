namespace LengthOfString.Core;

public class GetCatStrategies
{
    public static byte[] FromLocalCache(char character, string path)
    {
        var catFileNames = Directory.GetFiles(path);
        int index = character % catFileNames.Length;
        var catFileName = catFileNames[index];
        return File.ReadAllBytes(catFileName);
    }

    public static byte[] FromInternet(char character, int catHeight)
    {
        string _catsIdsFilename = "catIds.txt";
        var allCatIds = File.ReadAllLines(_catsIdsFilename);
        int index = character % allCatIds.Length;
        string catId = allCatIds[index];
        string urlToCat = $"https://cataas.com/cat/{catId}?height={catHeight}";
        using var client = new HttpClient();
        return client.GetByteArrayAsync(urlToCat).Result;
    }
}
