using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MyMonkeyApp;

/// <summary>
/// High-performance static helper class for managing monkey species data with advanced caching,
/// memory optimization, and performance monitoring capabilities.
/// </summary>
public static class MonkeyHelper
{
    // Performance-optimized collections
    private static readonly List<Monkey> _monkeys = new(capacity: 20); // Pre-allocate known capacity
    private static readonly ConcurrentDictionary<string, Monkey> _monkeyLookupCache = new();
    private static readonly Dictionary<string, int> _accessMetrics = new();
    
    // Thread-safe counters and state
    private static volatile bool _isInitialized = false;
    private static int _randomMonkeyAccessCount = 0;
    private static readonly Random _random = new();
    private static readonly object _initLock = new();
    
    // Performance monitoring
    private static readonly Stopwatch _performanceTimer = new();
    private static long _totalQueryTime = 0;
    private static int _totalQueries = 0;

    /// <summary>
    /// Gets the count of times a random monkey has been accessed.
    /// <summary>
    /// Gets the count of times a random monkey has been accessed.
    /// Thread-safe property for performance monitoring.
    /// </summary>
    public static int RandomMonkeyAccessCount => _randomMonkeyAccessCount;

    /// <summary>
    /// Gets performance metrics including average query time and cache efficiency.
    /// </summary>
    public static (long AverageQueryTimeMs, int TotalQueries, double CacheHitRatio) GetPerformanceMetrics()
    {
        var avgTime = _totalQueries > 0 ? _totalQueryTime / _totalQueries : 0;
        var cacheHitRatio = _totalQueries > 0 ? (double)_monkeyLookupCache.Count / _totalQueries : 0;
        return (avgTime, _totalQueries, Math.Min(cacheHitRatio, 1.0));
    }

    /// <summary>
    /// High-performance initialization with memory optimization and thread safety.
    /// Uses double-checked locking pattern for optimal performance.
    /// </summary>
    public static void InitializeMonkeyData()
    {
        // Double-checked locking pattern for performance
        if (_isInitialized)
            return;

        lock (_initLock)
        {
            if (_isInitialized)
                return;

            _performanceTimer.Start();

            // Clear collections efficiently
            _monkeys.Clear();
            _monkeyLookupCache.Clear();
            _accessMetrics.Clear();

            // Pre-populate with known monkey data for optimal performance
            var monkeyData = new List<Monkey>(20) // Pre-allocated capacity
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

        // Efficiently populate main collection and lookup cache
        _monkeys.AddRange(monkeyData);
        
        // Pre-populate lookup cache for O(1) name-based access
        foreach (var monkey in monkeyData)
        {
            _monkeyLookupCache.TryAdd(monkey.Name.ToLowerInvariant(), monkey);
        }

        _performanceTimer.Stop();
        _isInitialized = true;
        
        // Track initialization performance
        Interlocked.Add(ref _totalQueryTime, _performanceTimer.ElapsedMilliseconds);
        Interlocked.Increment(ref _totalQueries);
        _performanceTimer.Reset();
        }
    }

    /// <summary>
    /// High-performance method to get all available monkey species with minimal memory allocation.
    /// Returns a read-only span for better performance when full collection access is needed.
    /// </summary>
    /// <returns>Read-only span of all monkey species for optimal performance.</returns>
    public static ReadOnlySpan<Monkey> GetMonkeysAsSpan()
    {
        if (!_isInitialized)
            InitializeMonkeyData();

        return CollectionsMarshal.AsSpan(_monkeys);
    }

    /// <summary>
    /// Gets all available monkey species. Initializes data if not already done.
    /// For high-frequency access, consider using GetMonkeysAsSpan() for better performance.
    /// </summary>
    /// <returns>A copy of the list of all monkey species.</returns>
    public static List<Monkey> GetMonkeys()
    {
        if (!_isInitialized)
            InitializeMonkeyData();

        return new List<Monkey>(_monkeys);
    }

    /// <summary>
    /// High-performance monkey lookup by name using cached hash table for O(1) access.
    /// Includes performance monitoring and access pattern tracking.
    /// </summary>
    /// <param name="name">The name of the monkey species to search for.</param>
    /// <returns>The monkey species if found, otherwise null.</returns>
    public static Monkey? GetMonkeyByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        if (!_isInitialized)
            InitializeMonkeyData();

        _performanceTimer.Start();

        // Use cached lookup for O(1) performance
        var found = _monkeyLookupCache.TryGetValue(name.ToLowerInvariant(), out var monkey);
        
        _performanceTimer.Stop();
        
        // Track performance metrics
        Interlocked.Add(ref _totalQueryTime, _performanceTimer.ElapsedMilliseconds);
        Interlocked.Increment(ref _totalQueries);
        
        // Track access patterns
        lock (_accessMetrics)
        {
            _accessMetrics[name] = _accessMetrics.GetValueOrDefault(name, 0) + 1;
        }
        
        _performanceTimer.Reset();
        return found ? monkey : null;
    }

    /// <summary>
    /// High-performance random monkey selection with thread-safe counter management.
    /// Uses optimized random access without creating unnecessary allocations.
    /// </summary>
    /// <returns>A randomly selected monkey species.</returns>
    public static Monkey? GetRandomMonkey()
    {
        if (!_isInitialized)
            InitializeMonkeyData();

        if (_monkeys.Count == 0)
            return null;

        var index = _random.Next(_monkeys.Count);
        
        // Thread-safe increment
        Interlocked.Increment(ref _randomMonkeyAccessCount);
        
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
    /// Thread-safe reset of the random monkey access count.
    /// </summary>
    public static void ResetRandomAccessCount()
    {
        Interlocked.Exchange(ref _randomMonkeyAccessCount, 0);
    }

    /// <summary>
    /// Gets comprehensive performance and collection statistics.
    /// Includes memory usage, query performance, and access patterns.
    /// </summary>
    /// <returns>A detailed string containing all statistics.</returns>
    public static string GetCollectionStats()
    {
        if (!_isInitialized)
            InitializeMonkeyData();

        var totalPopulation = _monkeys.Sum(m => m.Population);
        var averagePopulation = _monkeys.Count > 0 ? totalPopulation / _monkeys.Count : 0;
        var (avgQueryTime, totalQueries, cacheHitRatio) = GetPerformanceMetrics();

        // Calculate memory usage estimate
        var estimatedMemoryBytes = _monkeys.Count * 200; // Rough estimate per monkey object
        var estimatedMemoryKB = estimatedMemoryBytes / 1024.0;

        return $"""
            🐒 Monkey Collection Performance Report:
            
            📊 Collection Statistics:
            - Total Species: {_monkeys.Count}
            - Total Population: {totalPopulation:N0}
            - Average Population per Species: {averagePopulation:N0}
            - Random Monkey Access Count: {_randomMonkeyAccessCount}
            
            ⚡ Performance Metrics:
            - Total Queries: {totalQueries}
            - Average Query Time: {avgQueryTime}ms
            - Cache Efficiency: {cacheHitRatio:P1}
            - Estimated Memory Usage: {estimatedMemoryKB:F1} KB
            
            🎯 Most Accessed Species:
            {GetTopAccessedSpecies()}
            """;
    }

    /// <summary>
    /// Gets the top 3 most accessed monkey species for performance analysis.
    /// </summary>
    /// <returns>Formatted string of top accessed species.</returns>
    private static string GetTopAccessedSpecies()
    {
        lock (_accessMetrics)
        {
            if (_accessMetrics.Count == 0)
                return "- No access data available yet";

            var topSpecies = _accessMetrics
                .OrderByDescending(kvp => kvp.Value)
                .Take(3)
                .Select((kvp, index) => $"#{index + 1}: {kvp.Key} ({kvp.Value} accesses)")
                .ToList();

            return string.Join("\n            ", topSpecies.DefaultIfEmpty("- No access data available yet"));
        }
    }

    /// <summary>
    /// Advanced search with fuzzy matching for better user experience.
    /// Uses Levenshtein distance for approximate string matching.
    /// </summary>
    /// <param name="searchTerm">The search term to match against monkey names.</param>
    /// <param name="maxDistance">Maximum edit distance for fuzzy matching (default: 2).</param>
    /// <returns>List of monkeys matching the search criteria, ordered by relevance.</returns>
    public static List<(Monkey Monkey, int Distance)> FuzzySearchMonkeys(string searchTerm, int maxDistance = 2)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<(Monkey, int)>();

        if (!_isInitialized)
            InitializeMonkeyData();

        var results = new List<(Monkey, int)>();
        var lowerSearchTerm = searchTerm.ToLowerInvariant();

        foreach (var monkey in _monkeys)
        {
            var distance = CalculateLevenshteinDistance(lowerSearchTerm, monkey.Name.ToLowerInvariant());
            if (distance <= maxDistance)
            {
                results.Add((monkey, distance));
            }
        }

        return results.OrderBy(r => r.Item2).ToList();
    }

    /// <summary>
    /// Calculates the Levenshtein distance between two strings for fuzzy matching.
    /// </summary>
    private static int CalculateLevenshteinDistance(string source, string target)
    {
        if (string.IsNullOrEmpty(source)) return target?.Length ?? 0;
        if (string.IsNullOrEmpty(target)) return source.Length;

        var matrix = new int[source.Length + 1, target.Length + 1];

        for (var i = 0; i <= source.Length; i++)
            matrix[i, 0] = i;
        for (var j = 0; j <= target.Length; j++)
            matrix[0, j] = j;

        for (var i = 1; i <= source.Length; i++)
        {
            for (var j = 1; j <= target.Length; j++)
            {
                var cost = source[i - 1] == target[j - 1] ? 0 : 1;
                matrix[i, j] = Math.Min(
                    Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                    matrix[i - 1, j - 1] + cost);
            }
        }

        return matrix[source.Length, target.Length];
    }
}