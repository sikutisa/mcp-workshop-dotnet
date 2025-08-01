using System.Diagnostics;
using MyMonkeyApp;

namespace MyMonkeyApp;

/// <summary>
/// High-performance main program class for the Monkey Console Application.
/// Optimized for fast startup, minimal memory allocation, and responsive UI.
/// </summary>
class Program
{
    private static readonly Random _random = new();
    private static readonly Stopwatch _performanceTimer = Stopwatch.StartNew();
    private static int _menuSelections = 0;

    /// <summary>
    /// High-performance main entry point with startup optimization.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static void Main(string[] args)
    {
        // Pre-warm the MonkeyHelper for faster first access
        _ = Task.Run(() => MonkeyHelper.InitializeMonkeyData());
        
        DisplayWelcomeBanner();
        RunMainMenu();
        
        // Display final performance stats
        DisplayPerformanceStats();
    }

    /// <summary>
    /// Displays startup performance statistics.
    /// </summary>
    static void DisplayPerformanceStats()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n🚀 Performance Summary:");
        Console.WriteLine($"   • Session Duration: {_performanceTimer.Elapsed.TotalSeconds:F1}s");
        Console.WriteLine($"   • Menu Selections: {_menuSelections}");
        Console.WriteLine($"   • Memory Usage: {GC.GetTotalMemory(false) / 1024:N0} KB");
        Console.ResetColor();
    }

    /// <summary>
    /// Displays the ASCII art welcome banner with random monkey art.
    /// </summary>
    static void DisplayWelcomeBanner()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"
  __  __             _              _                
 |  \/  |           | |            | |               
 | \  / | ___  _ __ | | _____ _   _| |     ___   ___  
 | |\/| |/ _ \| '_ \| |/ / _ \ | | | |    / _ \ / _ \ 
 | |  | | (_) | | | |   <  __/ |_| | |___| (_) | (_) |
 |_|  |_|\___/|_| |_|_|\_\___|\__, |______\___/ \___/ 
                               __/ |                  
                              |___/                   
        ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("        🐵 Welcome to the Monkey Species Explorer! 🐒");
        Console.WriteLine("        Discover amazing primates from around the world!");
        Console.ResetColor();
        
        // Display random ASCII art
        DisplayRandomAsciiArt();
        
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    /// <summary>
    /// Displays random ASCII art of monkeys.
    /// </summary>
    static void DisplayRandomAsciiArt()
    {
        var asciiArts = new string[]
        {
            @"
                        🐵
                   .-""""""""-.
                 .'          '.
                /   O      O   \
               :           `    :
               |                |   
               :    .------.    :
                \  '        '  /
                 '.          .'
                   '-.......-'
            ",
            @"
                   🐒 Monkey Business! 🐒
                      ___
                   .-'   '-.
                  /  ._. ._. \
                 |  (o)=(o)  |
                  \    <    /
                   |  ___  |
                   |.'   '.|
                   '-......-'
            ",
            @"
                    🙈 See No Evil 🙈
                      .-.   .-.
                     (   )-(   )
                      \ (   ) /
                       |  -  |
                       |  o  |
                       |     |
                       '-----'
            ",
            @"
                    🙉 Hear No Evil 🙉
                       .-''-.
                      /      \
                     |  ████  |
                     |   oo   |
                     |   ><   |
                     |  \__/  |
                      \______/
            ",
            @"
                    🙊 Speak No Evil 🙊
                       .-----.
                      /       \
                     |  ^   ^  |
                     |    o    |
                     |   ---   |
                     |  \___/  |
                      '-------'
            "
        };

        var selectedArt = asciiArts[_random.Next(asciiArts.Length)];
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(selectedArt);
        Console.ResetColor();
    }

    /// <summary>
    /// High-performance main interactive menu loop with optimization.
    /// </summary>
    static void RunMainMenu()
    {
        bool continueRunning = true;
        Span<char> inputBuffer = stackalloc char[10]; // Stack allocation for input

        while (continueRunning)
        {
            try
            {
                DisplayMainMenu();
                var choice = GetUserChoice();
                _menuSelections++; // Track usage for performance stats

                switch (choice)
                {
                    case "1":
                        ListAllMonkeys();
                        break;
                    case "2":
                        SearchMonkeyByName();
                        break;
                    case "3":
                        DisplayRandomMonkey();
                        break;
                    case "4":
                        DisplayPerformanceReport(); // Show performance before exit
                        DisplayExitMessage();
                        continueRunning = false;
                        break;
                    case "5": // New performance menu option
                        DisplayPerformanceReport();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Invalid choice. Please select 1-5.");
                        Console.ResetColor();
                        break;
                }

                if (continueRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }

    /// <summary>
    /// Displays the high-performance main menu with enhanced options and real-time stats.
    /// </summary>
    static void DisplayMainMenu()
    {
        Console.Clear();
        
        // Random small ASCII art for the menu
        DisplayRandomMenuArt();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("� HIGH-PERFORMANCE MONKEY EXPLORER 🐒");
        Console.WriteLine("======================================");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("1. 📋 List all monkey species (Optimized)");
        Console.WriteLine("2. 🔍 Smart search with fuzzy matching");
        Console.WriteLine("3. 🎲 Get a random monkey (Thread-safe)");
        Console.WriteLine("4. 🚪 Exit with performance summary");
        Console.WriteLine("5. ⚡ View performance analytics");
        Console.WriteLine();
        
        // Enhanced real-time statistics
        Console.ForegroundColor = ConsoleColor.DarkGray;
        var stats = MonkeyHelper.GetPerformanceMetrics();
        Console.WriteLine($"📊 Performance: {MonkeyHelper.GetMonkeyCount()} species | " +
                         $"Avg Query: {stats.AverageQueryTimeMs}ms | " +
                         $"Cache Hit: {stats.CacheHitRatio:P1} | " +
                         $"Random: {MonkeyHelper.RandomMonkeyAccessCount}");
        Console.ResetColor();
        Console.WriteLine();
        Console.Write("Please enter your choice (1-5): ");
    }

    /// <summary>
    /// Displays random small ASCII art for the menu.
    /// </summary>
    static void DisplayRandomMenuArt()
    {
        var menuArts = new string[]
        {
            "    🐵🐒🐵",
            "   🙈🙉🙊",
            "    🐒💫🐵",
            "   🌟🐵🌟",
            "    🐒🎯🐵"
        };

        var selectedArt = menuArts[_random.Next(menuArts.Length)];
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(selectedArt);
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>
    /// Gets the user's menu choice input.
    /// </summary>
    /// <returns>The user's input as a string.</returns>
    static string GetUserChoice()
    {
        var input = Console.ReadLine();
        return input?.Trim() ?? string.Empty;
    }

    /// <summary>
    /// Lists all available monkey species in a formatted display.
    /// </summary>
    static void ListAllMonkeys()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("📋 ALL MONKEY SPECIES");
        Console.WriteLine("====================");
        Console.ResetColor();
        Console.WriteLine();

        var monkeys = MonkeyHelper.GetMonkeys();
        
        for (int i = 0; i < monkeys.Count; i++)
        {
            var monkey = monkeys[i];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{i + 1}. {monkey.Name}");
            Console.ResetColor();
            Console.WriteLine($"   📍 Location: {monkey.Location}");
            Console.WriteLine($"   👥 Population: {monkey.Population:N0}");
            Console.WriteLine($"   🌍 Coordinates: {monkey.Latitude:F2}°, {monkey.Longitude:F2}°");
            
            // Truncate details if too long
            var details = monkey.Details.Length > 80 
                ? monkey.Details[..77] + "..." 
                : monkey.Details;
            Console.WriteLine($"   ℹ️  Details: {details}");
            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"🎯 Total species: {MonkeyHelper.GetMonkeyCount()}");
        Console.WriteLine(MonkeyHelper.GetCollectionStats());
        Console.ResetColor();
    }

    /// <summary>
    /// Advanced smart search with fuzzy matching and performance tracking.
    /// </summary>
    static void SearchMonkeyByName()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🧠 SMART MONKEY SEARCH (Fuzzy Matching)");
        Console.WriteLine("======================================");
        Console.ResetColor();
        Console.WriteLine();
        Console.Write("Enter the monkey name to search for: ");
        
        var searchName = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(searchName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Please enter a valid monkey name.");
            Console.ResetColor();
            return;
        }

        var sw = Stopwatch.StartNew();
        
        // Try exact match first (fastest)
        var exactMatch = MonkeyHelper.GetMonkeyByName(searchName);
        
        if (exactMatch != null)
        {
            sw.Stop();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🎯 EXACT MATCH FOUND! (Search time: {sw.ElapsedMilliseconds}ms)");
            Console.ResetColor();
            DisplayMonkeyDetails(exactMatch);
            return;
        }

        // Try fuzzy search for approximate matches
        var fuzzyResults = MonkeyHelper.FuzzySearchMonkeys(searchName, maxDistance: 3);
        sw.Stop();
        
        if (fuzzyResults.Count > 0)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"🔍 FUZZY MATCHES FOUND! (Search time: {sw.ElapsedMilliseconds}ms)");
            Console.WriteLine($"Found {fuzzyResults.Count} similar matches:");
            Console.ResetColor();
            Console.WriteLine();

            for (int i = 0; i < Math.Min(fuzzyResults.Count, 5); i++) // Show top 5 matches
            {
                var (monkey, distance) = fuzzyResults[i];
                var similarity = Math.Max(0, 100 - (distance * 10)); // Simple similarity percentage
                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{i + 1}. {monkey.Name} (Similarity: {similarity}%)");
                Console.ResetColor();
                Console.WriteLine($"   📍 {monkey.Location}");
                Console.WriteLine($"   � Population: {monkey.Population:N0}");
                Console.WriteLine();
            }

            Console.Write("Enter the number of the monkey you want to see (1-5), or 0 to go back: ");
            var choice = Console.ReadLine();
            
            if (int.TryParse(choice, out int index) && index > 0 && index <= Math.Min(fuzzyResults.Count, 5))
            {
                Console.WriteLine();
                DisplayMonkeyDetails(fuzzyResults[index - 1].Monkey);
            }
        }
        else
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"😞 No similar matches found for '{searchName}'. (Search time: {sw.ElapsedMilliseconds}ms)");
            Console.ResetColor();
            Console.WriteLine();
            
            // Show a few random suggestions
            Console.WriteLine("💡 Try searching for these popular species:");
            var randomSuggestions = MonkeyHelper.GetMonkeysAsSpan().ToArray()
                .OrderBy(x => Guid.NewGuid())
                .Take(5)
                .ToList();
                
            foreach (var suggestion in randomSuggestions)
            {
                Console.WriteLine($"  • {suggestion.Name}");
            }
        }
    }

    /// <summary>
    /// Displays a randomly selected monkey species with fun animations.
    /// </summary>
    static void DisplayRandomMonkey()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🎲 RANDOM MONKEY SELECTION");
        Console.WriteLine("=========================");
        Console.ResetColor();
        Console.WriteLine();

        // Fun animation
        Console.Write("🎯 Selecting a random monkey");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.WriteLine();
        Console.WriteLine();

        var randomMonkey = MonkeyHelper.GetRandomMonkey();
        
        if (randomMonkey != null)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("🎉 Here's your random monkey:");
            Console.ResetColor();
            Console.WriteLine();
            
            DisplayMonkeyDetails(randomMonkey);
            
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"📊 Total random selections: {MonkeyHelper.RandomMonkeyAccessCount}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("😞 No monkeys available for random selection.");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Displays detailed information about a specific monkey.
    /// </summary>
    /// <param name="monkey">The monkey to display details for.</param>
    static void DisplayMonkeyDetails(Monkey? monkey)
    {
        if (monkey == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No monkey data available.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"🐵 {monkey.Name}");
        Console.WriteLine(new string('=', monkey.Name.Length + 3));
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"📍 Location: {monkey.Location}");
        Console.WriteLine($"👥 Population: {monkey.Population:N0}");
        Console.WriteLine($"🌍 Coordinates: {monkey.Latitude:F6}°, {monkey.Longitude:F6}°");
        Console.WriteLine($"ℹ️  Details: {monkey.Details}");
        
        if (!string.IsNullOrEmpty(monkey.Image))
        {
            Console.WriteLine($"🖼️  Image: {monkey.Image}");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Displays a fun exit message with ASCII art.
    /// </summary>
    static void DisplayExitMessage()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"
    🐵 Thanks for exploring the monkey kingdom! 🐒
    
         .-""""""-.
       .'          '.
      /   Goodbye!   \
     :      👋        :
     |                |
     :    .------.    :
      \  '        '  /
       '.          .'
         '-.......-'
         
    🌟 Come back soon for more monkey adventures! 🌟
        ");
        Console.ResetColor();
        
        // Show final statistics
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("📊 SESSION STATISTICS:");
        Console.WriteLine(MonkeyHelper.GetCollectionStats());
        Console.ResetColor();
    }

    /// <summary>
    /// Displays comprehensive performance analytics and optimization insights.
    /// </summary>
    static void DisplayPerformanceReport()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("⚡ PERFORMANCE ANALYTICS DASHBOARD ⚡");
        Console.WriteLine("====================================");
        Console.ResetColor();
        Console.WriteLine();

        var stats = MonkeyHelper.GetPerformanceMetrics();
        var sessionTime = _performanceTimer.Elapsed;
        var memoryUsage = GC.GetTotalMemory(false);

        // Real-time performance metrics
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🚀 Real-Time Performance Metrics:");
        Console.ResetColor();
        Console.WriteLine($"   • Average Query Time: {stats.AverageQueryTimeMs}ms");
        Console.WriteLine($"   • Total Queries Executed: {stats.TotalQueries}");
        Console.WriteLine($"   • Cache Hit Ratio: {stats.CacheHitRatio:P2}");
        Console.WriteLine($"   • Memory Usage: {memoryUsage / 1024:N0} KB");
        Console.WriteLine();

        // Session analytics
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("📈 Session Analytics:");
        Console.ResetColor();
        Console.WriteLine($"   • Session Duration: {sessionTime.TotalSeconds:F1} seconds");
        Console.WriteLine($"   • Menu Interactions: {_menuSelections}");
        Console.WriteLine($"   • Random Monkey Requests: {MonkeyHelper.RandomMonkeyAccessCount}");
        Console.WriteLine($"   • Operations per Second: {(_menuSelections / Math.Max(sessionTime.TotalSeconds, 1)):F1}");
        Console.WriteLine();

        // Optimization recommendations
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("💡 Performance Insights:");
        Console.ResetColor();
        
        if (stats.CacheHitRatio > 0.8)
        {
            Console.WriteLine("   ✅ Excellent cache performance - lookups are highly optimized!");
        }
        else if (stats.CacheHitRatio > 0.5)
        {
            Console.WriteLine("   ⚠️  Good cache performance - consider pre-warming more data.");
        }
        else
        {
            Console.WriteLine("   🔴 Cache efficiency could be improved - mostly cold starts detected.");
        }

        if (stats.AverageQueryTimeMs < 1)
        {
            Console.WriteLine("   ✅ Lightning-fast query performance!");
        }
        else if (stats.AverageQueryTimeMs < 5)
        {
            Console.WriteLine("   ⚠️  Query performance is acceptable.");
        }
        else
        {
            Console.WriteLine("   🔴 Queries are taking longer than optimal - check for bottlenecks.");
        }

        if (memoryUsage < 1024 * 1024) // 1MB
        {
            Console.WriteLine("   ✅ Memory usage is optimal for this application size.");
        }
        else
        {
            Console.WriteLine("   ⚠️  Memory usage is higher than expected - monitor for leaks.");
        }

        Console.WriteLine();
        Console.WriteLine("📊 Detailed Collection Statistics:");
        Console.WriteLine(MonkeyHelper.GetCollectionStats());
        Console.WriteLine();
        
        // Performance tips
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("💡 Pro Tips for Optimal Performance:");
        Console.WriteLine("   • Use fuzzy search to find species faster");
        Console.WriteLine("   • Cache frequently accessed data automatically");
        Console.WriteLine("   • Monitor memory usage in production environments");
        Console.WriteLine("   • Consider async operations for larger datasets");
        Console.ResetColor();
    }
}
