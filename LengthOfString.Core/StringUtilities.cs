using System.Drawing;

namespace LengthOfString.Core;

public class StringUtilities
{
    private readonly string _tmpFolder;
    private readonly int _catHeight;
    private readonly int _maxImageWidth;
    private readonly Func<char, byte[]> _getCatStrategy;
    private readonly CatCounter _catCounter = Services.CatCounter;

    public StringUtilities(string tmpFolder, int catHeight, int maxImageWidth, Func<char, byte[]> getCatStrategy)
    {
        _tmpFolder = tmpFolder;
        _catHeight = catHeight;
        _maxImageWidth = maxImageWidth;
        _getCatStrategy = getCatStrategy;
    }

    public int Length(string input)
    {
        var unique = Guid.NewGuid();

        foreach (var c in input)
        {
            FetchCat(c, unique);
        }

        MergeCats(unique);

        int nr = CountCats(unique);

        return nr;
    }

    public void Cleanup()
    {
        if (!Directory.Exists(_tmpFolder))
        {
            return;
        }
        foreach (var file in Directory.GetFiles(_tmpFolder))
        {
            File.Delete(file);
        }
    }

    private void FetchCat(char character, Guid unique)
    {
        CreateDirectoryIfNotExist(_tmpFolder);
        byte[] fileBytes = _getCatStrategy(character);
        string fileName = $"{_tmpFolder}\\{unique}___{Guid.NewGuid()}.png";
        File.WriteAllBytes(fileName, fileBytes);
    }

    private void CreateDirectoryIfNotExist(string tmpFolder)
    {
        if (!Directory.Exists(_tmpFolder))
        {
            Directory.CreateDirectory(_tmpFolder);
        }
    }

    private void MergeCats(Guid unique)
    {
        var files = Directory.GetFiles(_tmpFolder, $"{unique}___*");
        var merged = MergeImages(files);
        merged.Save($"{_tmpFolder}\\{unique}.png");
    }

    private Bitmap MergeImages(string[] files)
    {
        var images = files.Select(file => Image.FromFile(file)).ToList();

        var widths = images.Select(images => images.Width).ToList();
        var totalWidth = widths.Sum();

        var grid = MathUtils.Flow(widths, _maxImageWidth);

        var nrOfRows = grid.Count;

        var imageWidth = Math.Min(_maxImageWidth, totalWidth);
        var imageHeight = _catHeight * nrOfRows;

        Bitmap bitmap = new(imageWidth, imageHeight);

        DrawCatsOnBitmap(images, grid, bitmap);

        return bitmap;
    }

    private void DrawCatsOnBitmap(List<Image> images, List<List<int>> grid, Bitmap bitmap)
    {
        using Graphics g = Graphics.FromImage(bitmap);

        int index = 0;

        var x = 0;
        var y = 0;

        foreach(var row in grid)
        {
            foreach (var width in row)
            {
                Image image = images[index];

                g.DrawImage(image, x, y, image.Width, _catHeight);

                x += width;
                index++;
            }
            x = 0;
            y += _catHeight;
        }
    }

    private int CountCats(Guid unique)
    {
        string fileName = $"{_tmpFolder}\\{unique}.png";
        var image = Image.FromFile(fileName);
        return _catCounter.Count(image);
    }
}
