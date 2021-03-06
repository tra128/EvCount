﻿using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace EVCount.Clases
{
    public class PokemonApi
    {
        string[] PokemonG1 = new string[] { "0","Bulbasaur", "Ivysaur", "Venusaur", "Charmander", "Charmeleon", "Charizard", "Squirtle", "Wartortle", "Blastoise", "Caterpie", "Metapod", "Butterfree", "Weedle", "Kakuna", "Beedrill", "Pidgey", "Pidgeotto", "Pidgeot", "Rattata", "Raticate", "Spearow", "Fearow", "Ekans", "Arbok", "Pikachu", "Raichu", "Sandshrew", "Sandslash", "Nidoran♀", "Nidorina", "Nidoqueen", "Nidoran♂", "Nidorino", "Nidoking", "Clefairy", "Clefable", "Vulpix", "Ninetales", "Jigglypuff", "Wigglytuff", "Zubat", "Golbat", "Oddish", "Gloom", "Vileplume", "Paras", "Parasect", "Venonat", "Venomoth", "Diglett", "Dugtrio", "Meowth", "Persian", "Psyduck", "Golduck", "Mankey", "Primeape", "Growlithe", "Arcanine", "Poliwag", "Poliwhirl", "Poliwrath", "Abra", "Kadabra", "Alakazam", "Machop", "Machoke", "Machamp", "Bellsprout", "Weepinbell", "Victreebel", "Tentacool", "Tentacruel", "Geodude", "Graveler", "Golem", "Ponyta", "Rapidash", "Slowpoke", "Slowbro", "Magnemite", "Magneton", "Farfetch’d", "Doduo", "Dodrio", "Seel", "Dewgong", "Grimer", "Muk", "Shellder", "Cloyster", "Gastly", "Haunter", "Gengar", "Onix", "Drowzee", "Hypno", "Krabby", "Kingler", "Voltorb", "Electrode", "Exeggcute", "Exeggutor", "Cubone", "Marowak", "Hitmonlee", "Hitmonchan", "Lickitung", "Koffing", "Weezing", "Rhyhorn", "Rhydon", "Chansey", "Tangela", "Kangaskhan", "Horsea", "Seadra", "Goldeen", "Seaking", "Staryu", "Starmie", "Mr. Mime", "Scyther", "Jynx", "Electabuzz", "Magmar", "Pinsir", "Tauros", "Magikarp", "Gyarados", "Lapras", "Ditto", "Eevee", "Vaporeon", "Jolteon", "Flareon", "Porygon", "Omanyte", "Omastar", "Kabuto", "Kabutops", "Aerodactyl", "Snorlax", "Articuno", "Zapdos", "Moltres", "Dratini", "Dragonair", "Dragonite", "Mewtwo", "Mew" };
        string[] PokemonG2 = new string[] { "151", "Chikorita", "Bayleef", "Meganium", "Cyndaquil", "Quilava", "Typhlosion", "Totodile", "Croconaw", "Feraligatr", "Sentret", "Furret", "Hoothoot", "Noctowl", "Ledyba", "Ledian", "Spinarak", "Ariados", "Crobat", "Chinchou", "Lanturn", "Pichu", "Cleffa", "Igglybuff", "Togepi", "Togetic", "Natu", "Xatu", "Mareep", "Flaaffy", "Ampharos", "Bellossom", "Marill", "Azumarill", "Sudowoodo", "Politoed", "Hoppip", "Skiploom", "Jumpluff", "Aipom", "Sunkern", "Sunflora", "Yanma", "Wooper", "Quagsire", "Espeon", "Umbreon", "Murkrow", "Slowking", "Misdreavus", "Unown", "Wobbuffet", "Girafarig", "Pineco", "Forretress", "Dunsparce", "Gligar", "Steelix", "Snubbull", "Granbull", "Qwilfish", "Scizor", "Shuckle", "Heracross", "Sneasel", "Teddiursa", "Ursaring", "Slugma", "Magcargo", "Swinub", "Piloswine", "Corsola", "Remoraid", "Octillery", "Delibird", "Mantine", "Skarmory", "Houndour", "Houndoom", "Kingdra", "Phanpy", "Donphan", "Porygon2", "Stantler", "Smeargle", "Tyrogue", "Hitmontop", "Smoochum", "Elekid", "Magby", "Miltank", "Blissey", "Raikou", "Entei", "Suicune", "Larvitar", "Pupitar", "Tyranitar", "Lugia", "Ho-Oh", "Celebi" };
        string[] PokemonG3 = new string[] { "251", "Treecko", "Grovyle", "Sceptile", "Torchic", "Combusken", "Blaziken", "Mudkip", "Marshtomp", "Swampert", "Poochyena", "Mightyena", "Zigzagoon", "Linoone", "Wurmple", "Silcoon", "Beautifly", "Cascoon", "Dustox", "Lotad", "Lombre", "Ludicolo", "Seedot", "Nuzleaf", "Shiftry", "Taillow", "Swellow", "Wingull", "Pelipper", "Ralts", "Kirlia", "Gardevoir", "Surskit", "Masquerain", "Shroomish", "Breloom", "Slakoth", "Vigoroth", "Slaking", "Nincada", "Ninjask", "Shedinja", "Whismur", "Loudred", "Exploud", "Makuhita", "Hariyama", "Azurill", "Nosepass", "Skitty", "Delcatty", "Sableye", "Mawile", "Aron", "Lairon", "Aggron", "Meditite", "Medicham", "Electrike", "Manectric", "Plusle", "Minun", "Volbeat", "Illumise", "Roselia", "Gulpin", "Swalot", "Carvanha", "Sharpedo", "Wailmer", "Wailord", "Numel", "Camerupt", "Torkoal", "Spoink", "Grumpig", "Spinda", "Trapinch", "Vibrava", "Flygon", "Cacnea", "Cacturne", "Swablu", "Altaria", "Zangoose", "Seviper", "Lunatone", "Solrock", "Barboach", "Whiscash", "Corphish", "Crawdaunt", "Baltoy", "Claydol", "Lileep", "Cradily", "Anorith", "Armaldo", "Feebas", "Milotic", "Castform", "Kecleon", "Shuppet", "Banette", "Duskull", "Dusclops", "Tropius", "Chimecho", "Absol", "Wynaut", "Snorunt", "Glalie", "Spheal", "Sealeo", "Walrein", "Clamperl", "Huntail", "Gorebyss", "Relicanth", "Luvdisc", "Bagon", "Shelgon", "Salamence", "Beldum", "Metang", "Metagross", "Regirock", "Regice", "Registeel", "Latias", "Latios", "Kyogre", "Groudon", "Rayquaza", "Jirachi", "Deoxys" };
        string[] PokemonG4 = new string[] { "386","Turtwig", "Grotle", "Torterra", "Chimchar", "Monferno", "Infernape", "Piplup", "Prinplup", "Empoleon", "Starly", "Staravia", "Staraptor", "Bidoof", "Bibarel", "Kricketot", "Kricketune", "Shinx", "Luxio", "Luxray", "Budew", "Roserade", "Cranidos", "Rampardos", "Shieldon", "Bastiodon", "Burmy", "Wormadam", "Mothim", "Combee", "Vespiquen", "Pachirisu", "Buizel", "Floatzel", "Cherubi", "Cherrim", "Shellos", "Gastrodon", "Ambipom", "Drifloon", "Drifblim", "Buneary", "Lopunny", "Mismagius", "Honchkrow", "Glameow", "Purugly", "Chingling", "Stunky", "Skuntank", "Bronzor", "Bronzong", "Bonsly", "Mime Jr.", "Happiny", "Chatot", "Spiritomb", "Gible", "Gabite", "Garchomp", "Munchlax", "Riolu", "Lucario", "Hippopotas", "Hippowdon", "Skorupi", "Drapion", "Croagunk", "Toxicroak", "Carnivine", "Finneon", "Lumineon", "Mantyke", "Snover", "Abomasnow", "Weavile", "Magnezone", "Lickilicky", "Rhyperior", "Tangrowth", "Electivire", "Magmortar", "Togekiss", "Yanmega", "Leafeon", "Glaceon", "Gliscor", "Mamoswine", "Porygon-Z", "Gallade", "Probopass", "Dusknoir", "Froslass", "Rotom", "Uxie", "Mesprit", "Azelf", "Dialga", "Palkia", "Heatran", "Regigigas", "Giratina", "Cresselia", "Phione", "Manaphy", "Darkrai", "Shaymin", "Arceus" };
        string[] PokemonG5 = new string[] { "Victini", "Snivy", "Servine", "Serperior", "Tepig", "Pignite", "Emboar", "Oshawott", "Dewott", "Samurott", "Patrat", "Watchog", "Lillipup", "Herdier", "Stoutland", "Purrloin", "Liepard", "Pansage", "Simisage", "Pansear", "Simisear", "Panpour", "Simipour", "Munna", "Musharna", "Pidove", "Tranquill", "Unfezant", "Blitzle", "Zebstrika", "Roggenrola", "Boldore", "Gigalith", "Woobat", "Swoobat", "Drilbur", "Excadrill", "Audino", "Timburr", "Gurdurr", "Conkeldurr", "Tympole", "Palpitoad", "Seismitoad", "Throh", "Sawk", "Sewaddle", "Swadloon", "Leavanny", "Venipede", "Whirlipede", "Scolipede", "Cottonee", "Whimsicott", "Petilil", "Lilligant", "Basculin", "Sandile", "Krokorok", "Krookodile", "Darumaka", "Darmanitan", "Maractus", "Dwebble", "Crustle", "Scraggy", "Scrafty", "Sigilyph", "Yamask", "Cofagrigus", "Tirtouga", "Carracosta", "Archen", "Archeops", "Trubbish", "Garbodor", "Zorua", "Zoroark", "Minccino", "Cinccino", "Gothita", "Gothorita", "Gothitelle", "Solosis", "Duosion", "Reuniclus", "Ducklett", "Swanna", "Vanillite", "Vanillish", "Vanilluxe", "Deerling", "Sawsbuck", "Emolga", "Karrablast", "Escavalier", "Foongus", "Amoonguss", "Frillish", "Jellicent", "Alomomola", "Joltik", "Galvantula", "Ferroseed", "Ferrothorn", "Klink", "Klang", "Klinklang", "Tynamo", "Eelektrik", "Eelektross", "Elgyem", "Beheeyem", "Litwick", "Lampent", "Chandelure", "Axew", "Fraxure", "Haxorus", "Cubchoo", "Beartic", "Cryogonal", "Shelmet", "Accelgor", "Stunfisk", "Mienfoo", "Mienshao", "Druddigon", "Golett", "Golurk", "Pawniard", "Bisharp", "Bouffalant", "Rufflet", "Braviary", "Vullaby", "Mandibuzz", "Heatmor", "Durant", "Deino", "Zweilous", "Hydreigon", "Larvesta", "Volcarona", "Cobalion", "Terrakion", "Virizion", "Tornadus", "Thundurus", "Reshiram", "Zekrom", "Landorus", "Kyurem", "Keldeo", "Meloetta", "Genesect" };
        string[] PokemonG6 = new string[] { "Chespin", "Quilladin", "Chesnaught", "Fennekin", "Braixen", "Delphox", "Froakie", "Frogadier", "Greninja", "Bunnelby", "Diggersby", "Fletchling", "Fletchinder", "Talonflame", "Scatterbug", "Spewpa", "Vivillon", "Litleo", "Pyroar", "Flabébé", "Floette", "Florges", "Skiddo", "Gogoat", "Pancham", "Pangoro", "Furfrou", "Espurr", "Meowstic", "Honedge", "Doublade", "Aegislash", "Spritzee", "Aromatisse", "Swirlix", "Slurpuff", "Inkay", "Malamar", "Binacle", "Barbaracle", "Skrelp", "Dragalge", "Clauncher", "Clawitzer", "Helioptile", "Heliolisk", "Tyrunt", "Tyrantrum", "Amaura", "Aurorus", "Sylveon", "Hawlucha", "Dedenne", "Carbink", "Goomy", "Sliggoo", "Goodra", "Klefki", "Phantump", "Trevenant", "Pumpkaboo", "Gourgeist", "Bergmite", "Avalugg", "Noibat", "Noivern", "Xerneas", "Yveltal", "Zygarde", "Diancie", "Hoopa", "Volcanion" };
        string[] PokemonG7 = new string[] { "Rowlet", "Dartrix", "Decidueye", "Litten", "Torracat", "Incineroar", "Popplio", "Brionne", "Primarina", "Pikipek", "Trumbeak", "Toucannon", "Yungoos", "Gumshoos", "Grubbin", "Charjabug", "Vikavolt", "Crabrawler", "Crabominable", "Oricorio", "Cutiefly", "Ribombee", "Rockruff", "Lycanroc", "Wishiwashi", "Mareanie", "Toxapex", "Mudbray", "Mudsdale", "Dewpider", "Araquanid", "Fomantis", "Lurantis", "Morelull", "Shiinotic", "Salandit", "Salazzle", "Stufful", "Bewear", "Bounsweet", "Steenee", "Tsareena", "Comfey", "Oranguru", "Passimian", "Wimpod", "Golisopod", "Sandygast", "Palossand", "Pyukumuku", "Type: Null", "Silvally", "Minior", "Komala", "Turtonator", "Togedemaru", "Mimikyu", "Bruxish", "Drampa", "Dhelmise", "Jangmo-o", "Hakamo-o", "Kommo-o", "Tapu Koko", "Tapu Lele", "Tapu Bulu", "Tapu Fini", "Cosmog", "Cosmoem", "Solgaleo", "Lunala", "Nihilego", "Buzzwole", "Pheromosa", "Xurkitree", "Celesteela", "Kartana", "Guzzlord", "Necrozma", "Magearna", "Marshadow", "Poipole", "Naganadel", "Stakataka", "Blacephalon", "Zeraora", };
        public string[] selectedGen;
        public int toAdd = 0;
        HttpClient Client;
        string EvJson = "https://api.jsonbin.io/b/5c870d9cbb08b22a75681c2e";
        string Gen1Json = "https://api.jsonbin.io/b/5c97e73c1707511ebd2a91ad";
        string Gen2Json = "https://api.jsonbin.io/b/5c97e7e21707511ebd2a9202";
        string Gen3Json = "https://api.jsonbin.io/b/5c97e8294dbf631e8b89b2b5";
        string Gen4Json = "https://api.jsonbin.io/b/5c97e8671bdf051e85390753";
        public PokemonApi()
        {
            Client = new HttpClient();
            Client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Pokemon>> Pokemons(string Gen)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string GenSelected = Path.Combine(path, "Gen.txt");
            bool Doit = false;
            int GenNumberFrom = 0;
            int GenNumberTo=1;
            int GenCurrentNumber = 0;
            List<Pokemon> Result = null;
            List<Pokemon> RealResult = new List<Pokemon>();
            string RestUrl = "";
            switch (Gen)
            {
                case "Gen1":
                    GenNumberFrom = 0;
                    GenNumberTo = 151;
                    selectedGen = PokemonG1;
                    RestUrl = Gen1Json;
                    File.WriteAllLines(GenSelected, PokemonG1);
                    break;
                case "Gen2":
                    GenNumberFrom = 151;
                    GenNumberTo = 251;
                    toAdd = 151;
                    selectedGen = PokemonG2;
                    RestUrl = Gen2Json;
                    File.WriteAllLines(GenSelected, PokemonG2);
                    break;
                case "Gen3":
                    GenNumberFrom = 251;
                    GenNumberTo = 386;
                    toAdd = 251;
                    selectedGen = PokemonG3;
                    RestUrl = Gen3Json;
                    File.WriteAllLines(GenSelected, PokemonG3);
                    break;
                case "Gen4":
                    RestUrl = Gen4Json;
                    selectedGen = PokemonG4;
                    File.WriteAllLines(GenSelected, PokemonG4);
                    break;
                case "Gen5":
                    break;
            }
            
            Debug.Write("GetAutoFill:--------Url Created " + RestUrl);
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            Debug.Write("GetAutoFill:--------Uri Created");
            Debug.Write("GetAutoFill:--------Uri Sent");
            var response = await Client.GetAsync(uri);
            Debug.Write("GetAutoFill:--------Uri answered");
            if (response.IsSuccessStatusCode)
            {
                Debug.Write("GetAutoFill:--------success");
                var content = await response.Content.ReadAsStringAsync();
                Debug.Write("GetAutoFill:--------Uri transform");
                Result = JsonConvert.DeserializeObject<List<Pokemon>>(content);
                /*foreach (Pokemon pkmn in Result)
                {
                    if (GenCurrentNumber >= GenNumberFrom)
                    {
                        Doit = true;
                    }
                    else { GenCurrentNumber += 1; }
                    if (Doit) { 
                        if (GenNumberFrom < GenNumberTo)
                        {
                            RealResult.Add(pkmn);
                            GenNumberFrom++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                }*/
            }
            return Result;

        }

        public List<PowerItem> GetPowerItems()
        {
            List<PowerItem> powerItems = new List<PowerItem>();
            powerItems.Add(new PowerItem { Name = "Macho Brace", Image = "machobrace.png" , AffectedStat="Ev x2"});
            powerItems.Add(new PowerItem { Name = "Power Weight", Image = "powerweight", AffectedStat = "+HP" });
            powerItems.Add(new PowerItem { Name = "Power Bracer", Image = "powerbracer", AffectedStat = "+Atk" });
            powerItems.Add(new PowerItem { Name = "Power Belt", Image = "powerbelt", AffectedStat = "+Def" });
            powerItems.Add(new PowerItem { Name = "Power Lens", Image = "powerlens", AffectedStat = "+Sp.Atk" });
            powerItems.Add(new PowerItem { Name = "Power Band", Image = "powerband.png", AffectedStat = "+Sp.Def" });
            powerItems.Add(new PowerItem { Name = "Power Anklet", Image = "poweranklet", AffectedStat = "+Spd" });
            powerItems.Add(new PowerItem { Name = "None", Image = "PokeballIcon", AffectedStat = "" });
            return powerItems;
        }

        public List<UsableItems> GetVitamins()
        {
            List<UsableItems> usableItems = new List<UsableItems>();
            usableItems.Add(new UsableItems { Name = "HP Up", Image = "hpup", AffectedStat = "+10HP", Value = 10 });
            usableItems.Add(new UsableItems { Name = "Protein", Image = "protein", AffectedStat = "+10Atk", Value = 10 });
            usableItems.Add(new UsableItems { Name = "Iron", Image = "iron", AffectedStat = "+10Def", Value = 10 });
            usableItems.Add(new UsableItems { Name = "Calcium", Image = "calcium", AffectedStat = "+10Sp.Atk", Value = 10 });
            usableItems.Add(new UsableItems { Name = "Zinc", Image = "zinc", AffectedStat = "+10Sp.Def", Value = 10 });
            usableItems.Add(new UsableItems { Name = "Carbos", Image = "carbos", AffectedStat = "+10Spd", Value = 10 });
            return usableItems;
        }

        public List<UsableItems> GetWings()
        {
            List<UsableItems> usableItems = new List<UsableItems>();
            usableItems.Add(new UsableItems { Name = "Health Wing", Image = "healthwing", AffectedStat = "+1HP", Value = 1 });
            usableItems.Add(new UsableItems { Name = "Muscle Wing", Image = "musclewing", AffectedStat = "+1Atk", Value = 1 });
            usableItems.Add(new UsableItems { Name = "Resist Wing", Image = "resistwing", AffectedStat = "+1Def", Value = 1 });
            usableItems.Add(new UsableItems { Name = "Genius Wing", Image = "geniuswing", AffectedStat = "+1Sp.Atk", Value = 1 });
            usableItems.Add(new UsableItems { Name = "Clever Wing", Image = "cleverwing", AffectedStat = "+1Sp.Def", Value = 1 });
            usableItems.Add(new UsableItems { Name = "Swift Wing", Image = "swiftwing", AffectedStat = "+1Spd", Value = 1 });
            return usableItems;
        }

        public List<UsableItems> GetBerries() {
            List<UsableItems> usableItems = new List<UsableItems>();
            usableItems.Add(new UsableItems { Name = "Pomeg Berry", Image = "pomegberry", AffectedStat = "-10HP", Value = -10 });
            usableItems.Add(new UsableItems { Name = "Kelpsy Berry", Image = "kelpsyberry", AffectedStat = "-10Atk", Value = -10 });
            usableItems.Add(new UsableItems { Name = "Qualot Berry", Image = "qualotberry", AffectedStat = "-10Def", Value = -10 });
            usableItems.Add(new UsableItems { Name = "Hondew Berry", Image = "hondewberry", AffectedStat = "-10Sp.Atk", Value = -10 });
            usableItems.Add(new UsableItems { Name = "Grepa Berry", Image = "grepaberry", AffectedStat = "-10Sp.Def", Value = -10 });
            usableItems.Add(new UsableItems { Name = "Tamato Berry", Image = "tamatoberry", AffectedStat = "-10Spd", Value = -10 });
            return usableItems;
        }

        public async void Generate()
        {
            

        }
    }

    public class Pokemon : INotifyPropertyChanged 
    {
        public string Image { get; set; }
        string name;
        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                var PName = value as string;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string GenSelected = Path.Combine(path, "Gen.txt");
                string[] selectedGen = File.ReadAllLines(GenSelected);
                for (int i=1; i<selectedGen.Length;i++)
                {
                    if (PName.Equals(selectedGen[i]))
                    {
                        Image = "https://raw.githubusercontent.com/tra128/EvCountAssets/master/Icon/"+(int.Parse(selectedGen[0])+i)+".png";
                        break;
                    }
                }
                OnPropertyChanged("Name");
            }
        }
        public int HP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int SpAtk { get; set; }
        public int SpDef { get; set; }
        public int Spd { get; set; }
        public int Total { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }

    public class Trainer
    {
        public string SaveID { get; set; }
        public string Image { get; set; }
        public String Name { get; set; }
        public string Pokemon { get; set; }
        public int HP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int SpAtk { get; set; }
        public int SpDef { get; set; }
        public int Spd { get; set; }
    }
    public class PowerItem
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string AffectedStat { get; set; }
    }
    public class UsableItems
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string AffectedStat { get; set; }
        public int Value { get; set; }
    }
    public class Diccion : INotifyPropertyChanged
    {
        string word;
        public String Word
        {
            get { return word; }
            set
            {
                word = value;
                OnPropertyChanged("Word");
            }
        }
        public string Imagen { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
