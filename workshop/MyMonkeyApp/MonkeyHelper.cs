namespace MyMonkeyApp;

/// <summary>
/// Static helper class for managing monkey species data and providing access methods.
/// </summary>
public static class MonkeyHelper
{
    private static readonly List<Monkey> _monkeys = new()
    {
        new Monkey
        {
            Name = "Baboon",
            Location = "Africa & Asia",
            Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg",
            Population = 10000
        },
        new Monkey
        {
            Name = "Capuchin Monkey",
            Location = "Central & South America",
            Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg",
            Population = 23000
        },
        new Monkey
        {
            Name = "Blue Monkey",
            Location = "Central and East Africa",
            Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg",
            Population = 200000
        },
        new Monkey
        {
            Name = "Squirrel Monkey",
            Location = "Central & South America",
            Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg",
            Population = 300000
        },
        new Monkey
        {
            Name = "Golden Lion Tamarin",
            Location = "Brazil",
            Details = "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Golden_lion_tamarin_portrait3.jpg/220px-Golden_lion_tamarin_portrait3.jpg",
            Population = 3000
        },
        new Monkey
        {
            Name = "Howler Monkey",
            Location = "Central & South America",
            Details = "Howler monkeys are among the largest of the New World monkeys. Fifteen species are currently recognised. Previously classified in the family Cebidae, they are now placed in the family Atelidae.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Alouatta_guariba.jpg/200px-Alouatta_guariba.jpg",
            Population = 25000
        },
        new Monkey
        {
            Name = "Japanese Macaque",
            Location = "Japan",
            Details = "The Japanese macaque, is a terrestrial Old World monkey species that is native to Japan. They are also sometimes known as the snow monkey because they live in areas where snow covers the ground for months each year",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Macaca_fuscata_fuscata1.jpg/220px-Macaca_fuscata_fuscata1.jpg",
            Population = 120000
        },
        new Monkey
        {
            Name = "Mandrill",
            Location = "Equatorial Africa",
            Details = "The mandrill is a primate of the Old World monkey family, closely related to the baboons and even more closely to the drill. It is found in southern Cameroon, Equatorial Guinea, Gabon, and the Republic of the Congo.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Mandrill_at_san_francisco_zoo.jpg/220px-Mandrill_at_san_francisco_zoo.jpg",
            Population = 720000
        },
        new Monkey
        {
            Name = "Proboscis Monkey",
            Location = "Borneo",
            Details = "The proboscis monkey or long-nosed monkey, known as the bekantan in Indonesia, is a reddish-brown arboreal Old World monkey with an unusually large nose only found on the southeast Asian island of Borneo.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Proboscis_Monkey_in_Borneo.jpg/250px-Proboscis_Monkey_in_Borneo.jpg",
            Population = 7000
        },
        new Monkey
        {
            Name = "Red-shanked Douc",
            Location = "Vietnam, Laos",
            Details = "The red-shanked douc is a species of Old World monkey, among the most colourful of all primates. This monkey is sometimes called the costumed ape for its extravagant appearance.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Portrait_of_a_Douc.jpg/159px-Portrait_of_a_Douc.jpg",
            Population = 550
        },
        new Monkey
        {
            Name = "Gray-shanked Douc",
            Location = "Vietnam",
            Details = "The gray-shanked douc langur is a douc species native to the Vietnamese provinces of Quang Nam, Quang Ngai, Binh Dinh, Kon Tum, and Gia Lai. The total population is estimated at 550 to 700 individuals.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Cuc.Phuong.Primate.Rehab.center.jpg/220px-Cuc.Phuong.Primate.Rehab.center.jpg",
            Population = 700
        },
        new Monkey
        {
            Name = "Gelada",
            Location = "Ethiopia",
            Details = "The gelada, sometimes called the bleeding-heart monkey or the gelada baboon, is a species of Old World monkey found only in the Ethiopian Highlands, with large populations in the Semien Mountains.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/Gelada-Pavian.jpg/320px-Gelada-Pavian.jpg",
            Population = 60000
        },
        new Monkey
        {
            Name = "Vervet Monkey",
            Location = "Southern and East Africa",
            Details = "The vervet monkey, or simply vervet, is an Old World monkey of the family Cercopithecidae native to Africa. The term \"vervet\" is also used to refer to all the members of the genus Chlorocebus.",
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/72/Vervet_monkey_Kruger.jpg/220px-Vervet_monkey_Kruger.jpg",
            Population = 500000
        }
    };

    /// <summary>
    /// Gets all available monkey species.
    /// </summary>
    /// <returns>A list of all monkey species.</returns>
    public static List<Monkey> GetMonkeys()
    {
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

        return _monkeys.FirstOrDefault(m => 
            m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets a random monkey species from the available collection.
    /// </summary>
    /// <returns>A randomly selected monkey species.</returns>
    public static Monkey GetRandomMonkey()
    {
        var random = new Random();
        var index = random.Next(_monkeys.Count);
        return _monkeys[index];
    }

    /// <summary>
    /// Gets the total count of available monkey species.
    /// </summary>
    /// <returns>The number of monkey species in the collection.</returns>
    public static int GetMonkeyCount()
    {
        return _monkeys.Count;
    }
}