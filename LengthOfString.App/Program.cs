using LengthOfString.Core;

Setup();
Go();

static void Go()
{
    Console.WriteLine();

    Services.StringUtilities.Cleanup();

    string testString = "happybits"; 

    int length = testString.Length();
    int oldSchoolLength = testString.Length;

    Console.ForegroundColor = ConsoleColor.White;

    Console.WriteLine();
    Console.WriteLine($"The length of string       '{testString}' is {length}");
    Console.WriteLine($"Oldschool length of string '{testString}' is {oldSchoolLength}");
}

void Setup()
{
    var key = File.ReadAllText("SubscriptionKey.txt");

    Services.Log = new Log(
        active: true
    );

    Services.CatCounter = new CatCounter(
        subscriptionKey: key,
        endpoint: "https://happybits-vision.cognitiveservices.azure.com/",
        catishWords: new[] { "cat", "mammal", "dog", "animal", "rabbit" }
        );

    Services.StringUtilities = new StringUtilities(
        tmpFolder: "tmp",
        catHeight:300,
        maxImageWidth: 1000,
        //getCatStrategy:(char c)=>GetCatStrategies.FromInternet(c, catHeight: 300)
        getCatStrategy:(char c)=>GetCatStrategies.FromLocalCache(c, path: "Catche")
        );

}
