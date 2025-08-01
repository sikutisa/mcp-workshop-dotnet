using MyMonkeyApp;

namespace MyMonkeyApp;

/// <summary>
/// Main program class for the Monkey Console Application.
/// </summary>
class Program
{
    private static readonly Random _random = new();

    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static void Main(string[] args)
    {
        DisplayWelcomeBanner();
        RunMainMenu();
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
    /// Runs the main interactive menu loop.
    /// </summary>
    static void RunMainMenu()
    {
        bool continueRunning = true;

        while (continueRunning)
        {
            try
            {
                DisplayMainMenu();
                var choice = GetUserChoice();

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
                        DisplayExitMessage();
                        continueRunning = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please select 1-4.");
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
    /// Displays the main menu options with statistics.
    /// </summary>
    static void DisplayMainMenu()
    {
        Console.Clear();
        
        // Random small ASCII art for the menu
        DisplayRandomMenuArt();
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🐵 MONKEY SPECIES EXPLORER MENU 🐒");
        Console.WriteLine("==================================");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("1. 📋 List all monkey species");
        Console.WriteLine("2. 🔍 Search for a specific monkey");
        Console.WriteLine("3. 🎲 Get a random monkey");
        Console.WriteLine("4. 🚪 Exit");
        Console.WriteLine();
        
        // Display statistics
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"📊 Stats: {MonkeyHelper.GetMonkeyCount()} species available | Random selections: {MonkeyHelper.RandomMonkeyAccessCount}");
        Console.ResetColor();
        Console.WriteLine();
        Console.Write("Please enter your choice (1-4): ");
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
    /// Allows the user to search for a specific monkey by name.
    /// </summary>
    static void SearchMonkeyByName()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("🔍 SEARCH FOR A MONKEY");
        Console.WriteLine("=====================");
        Console.ResetColor();
        Console.WriteLine();
        Console.Write("Enter the monkey name to search for: ");
        
        var searchName = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(searchName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please enter a valid monkey name.");
            Console.ResetColor();
            return;
        }

        var monkey = MonkeyHelper.GetMonkeyByName(searchName);
        
        if (monkey != null)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("🐵 MONKEY FOUND!");
            Console.ResetColor();
            DisplayMonkeyDetails(monkey);
        }
        else
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"😞 No monkey found with the name '{searchName}'.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Available monkey species:");
            var monkeys = MonkeyHelper.GetMonkeys();
            foreach (var m in monkeys)
            {
                Console.WriteLine($"  • {m.Name}");
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
}
