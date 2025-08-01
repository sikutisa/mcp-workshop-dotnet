namespace MyMonkeyApp;

/// <summary>
/// Static helper class for managing monkey species data from the MCP server and providing access methods.
/// </summary>
public static class MonkeyHelper
{
    private static readonly List<Monkey> _monkeys = new();
    private static int _randomMonkeyAccessCount = 0;
    private static bool _isInitialized = false;
    private static readonly Random _random = new();

    /// <summary>
    /// Gets the count of times a random monkey has been accessed.
    /// </summary>
    public static int RandomMonkeyAccessCount => _randomMonkeyAccessCount;

    /// <summary>
    /// Initializes the monkey collection with data from the MCP server.
    /// This method populates the collection with real monkey data.
    /// </summary>
    public static void InitializeMonkeyData()
    {
        if (_isInitialized)
            return;

        // Clear any existing data
        _monkeys.Clear();

        // Add monkey data from MCP server - this would be populated from actual MCP server calls
        var monkeyData = new List<Monkey>
        {
            new Monkey
            {
                Name = "Baboon",
                Location = "Africa & Asia",
                Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/baboon.jpg",
                Population = 10000,
                Latitude = -8.783195,
                Longitude = 34.508523
            },
            new Monkey
            {
                Name = "Capuchin Monkey",
                Location = "Central & South America",
                Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/capuchin.jpg",
                Population = 23000,
                Latitude = 12.769013,
                Longitude = -85.602364
            },
            new Monkey
            {
                Name = "Blue Monkey",
                Location = "Central and East Africa",
                Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/bluemonkey.jpg",
                Population = 12000,
                Latitude = 1.957709,
                Longitude = 37.297204
            },
            new Monkey
            {
                Name = "Squirrel Monkey",
                Location = "Central & South America",
                Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/saimiri.jpg",
                Population = 11000,
                Latitude = -8.783195,
                Longitude = -55.491477
            },
            new Monkey
            {
                Name = "Golden Lion Tamarin",
                Location = "Brazil",
                Details = "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/tamarin.jpg",
                Population = 19000,
                Latitude = -14.235004,
                Longitude = -51.92528
            },
            new Monkey
            {
                Name = "Howler Monkey",
                Location = "South America",
                Details = "Howler monkeys are among the largest of the New World monkeys. Fifteen species are currently recognised. Previously classified in the family Cebidae, they are now placed in the family Atelidae.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/alouatta.jpg",
                Population = 8000,
                Latitude = -8.783195,
                Longitude = -55.491477
            },
            new Monkey
            {
                Name = "Japanese Macaque",
                Location = "Japan",
                Details = "The Japanese macaque, is a terrestrial Old World monkey species native to Japan. They are also sometimes known as the snow monkey because they live in areas where snow covers the ground for months each",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/macasa.jpg",
                Population = 1000,
                Latitude = 36.204824,
                Longitude = 138.252924
            },
            new Monkey
            {
                Name = "Mandrill",
                Location = "Southern Cameroon, Gabon, and Congo",
                Details = "The mandrill is a primate of the Old World monkey family, closely related to the baboons and even more closely to the drill. It is found in southern Cameroon, Gabon, Equatorial Guinea, and Congo.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/mandrill.jpg",
                Population = 17000,
                Latitude = 7.369722,
                Longitude = 12.354722
            },
            new Monkey
            {
                Name = "Proboscis Monkey",
                Location = "Borneo",
                Details = "The proboscis monkey or long-nosed monkey, known as the bekantan in Malay, is a reddish-brown arboreal Old World monkey that is endemic to the south-east Asian island of Borneo.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/borneo.jpg",
                Population = 15000,
                Latitude = 0.961883,
                Longitude = 114.55485
            },
            new Monkey
            {
                Name = "Sebastian",
                Location = "Seattle",
                Details = "This little trouble maker lives in Seattle with James and loves traveling on adventures with James and tweeting @MotzMonkeys. He by far is an Android fanboy and is getting ready for the new Google Pixel 9!",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/sebastian.jpg",
                Population = 1,
                Latitude = 47.606209,
                Longitude = -122.332071
            },
            new Monkey
            {
                Name = "Henry",
                Location = "Phoenix",
                Details = "An adorable Monkey who is traveling the world with Heather and live tweets his adventures @MotzMonkeys. His favorite platform is iOS by far and is excited for the new iPhone Xs!",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/henry.jpg",
                Population = 1,
                Latitude = 33.448377,
                Longitude = -112.074037
            },
            new Monkey
            {
                Name = "Red-shanked douc",
                Location = "Vietnam",
                Details = "The red-shanked douc is a species of Old World monkey, among the most colourful of all primates. The douc is an arboreal and diurnal monkey that eats and sleeps in the trees of the forest.",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/douc.jpg",
                Population = 1300,
                Latitude = 16.111648,
                Longitude = 108.262122
            },
            new Monkey
            {
                Name = "Mooch",
                Location = "Seattle",
                Details = "An adorable Monkey who is traveling the world with Heather and live tweets his adventures @MotzMonkeys. Her favorite platform is iOS by far and is excited for the new iPhone 16!",
                Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/Mooch.PNG",
                Population = 1,
                Latitude = 47.608013,
                Longitude = -122.335167
            }
        };

        _monkeys.AddRange(monkeyData);
        _isInitialized = true;
    }

    /// <summary>
    /// Gets all available monkey species. Initializes data if not already done.
    /// </summary>
    /// <returns>A copy of the list of all monkey species.</returns>
    public static List<Monkey> GetMonkeys()
    {
        if (!_isInitialized)
            InitializeMonkeyData();

        return new List<Monkey>(_monkeys);
    }

    /// <summary>
    /// Gets a specific monkey species by name (case-insensitive search).
    /// </summary>
    /// <param name="name">The name of the monkey species to search for.</param>
    /// <returns>The monkey species if found, otherwise null.</returns>
    public static Monkey? GetMonkeyByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        if (!_isInitialized)
            InitializeMonkeyData();

        return _monkeys.FirstOrDefault(m => 
            m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets a random monkey species from the available collection and increments the access count.
    /// </summary>
    /// <returns>A randomly selected monkey species.</returns>
    public static Monkey? GetRandomMonkey()
    {
        if (!_isInitialized)
            InitializeMonkeyData();

        if (_monkeys.Count == 0)
            return null;

        var index = _random.Next(_monkeys.Count);
        _randomMonkeyAccessCount++;
        return _monkeys[index];
    }

    /// <summary>
    /// Gets the total count of available monkey species.
    /// </summary>
    /// <returns>The number of monkey species in the collection.</returns>
    public static int GetMonkeyCount()
    {
        if (!_isInitialized)
            InitializeMonkeyData();

        return _monkeys.Count;
    }

    /// <summary>
    /// Resets the random monkey access count to zero.
    /// </summary>
    public static void ResetRandomAccessCount()
    {
        _randomMonkeyAccessCount = 0;
    }

    /// <summary>
    /// Gets statistics about the monkey collection.
    /// </summary>
    /// <returns>A string containing collection statistics.</returns>
    public static string GetCollectionStats()
    {
        if (!_isInitialized)
            InitializeMonkeyData();

        var totalPopulation = _monkeys.Sum(m => m.Population);
        var averagePopulation = _monkeys.Count > 0 ? totalPopulation / _monkeys.Count : 0;

        return $"""
            Monkey Collection Statistics:
            - Total Species: {_monkeys.Count}
            - Total Population: {totalPopulation:N0}
            - Average Population per Species: {averagePopulation:N0}
            - Random Monkey Access Count: {_randomMonkeyAccessCount}
            """;
    }
}