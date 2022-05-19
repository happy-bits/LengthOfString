using System.Drawing;

namespace LengthOfString.Core
{

    public class CatDownloader
    {
        private readonly CatCounter _catCounter = Services.CatCounter;

        public void ToSave()
        {
            Directory.CreateDirectory("tmp\\goodcats");

            foreach (var id in File.ReadAllLines("catIds.txt"))
            {
                DownloadCat(id);
            }

            foreach (var id in File.ReadAllLines("catIds.txt"))
            {
                string fileName = $"tmp\\{id}.png";

                if (IsCat(fileName))
                {
                    File.AppendAllLines("tmp\\saveIds.txt", new[] { id });

                    File.Copy(fileName, $"tmp\\goodcats\\{id}.png");
                }
            }
        }

        private static void DownloadCat(string catId)
        {
            string urlToCat = $"https://cataas.com/cat/{catId}?height={300}";

            string fileName = $"tmp\\{catId}.png";

            using var client = new HttpClient();
            byte[] fileBytes = client.GetByteArrayAsync(urlToCat).Result;
            File.WriteAllBytes(fileName, fileBytes);
        }


        private bool IsCat(string fileName)
        {
            return _catCounter.IsGoodCat(Image.FromFile(fileName));
        }
    }
}
