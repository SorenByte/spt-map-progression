using System.Collections.Generic;
using BepInEx.Configuration;
using SPTMapProgression.Config;
using SPTMapProgression.MapProgression;

namespace SPTMapProgression.Config;

public class BepinConfigDefault(ConfigFile config, MapProgressionManager mapProgressionManager) : BepinConfig
{
    internal readonly ConfigFile Config = config;
    internal readonly MapProgressionManager MapProgressionManager = mapProgressionManager;
    
    public override void Init()
    {
        MapRequirements =
            new Dictionary<string, (ConfigEntry<string> quest, ConfigEntry<int> level, ConfigEntry<bool> transit)>();
        
        ShouldPlaySound = Config.Bind(
            "General Settings",
            "Play Map Unlock Sound",
            true,
            "Whether or not to play the unlock sound when you view the map screen for the first time after unlocking a location");
            
        LockedText = Config.Bind(
            "Requirements Display Text",
            "Location Locked",
            "{0} is Locked!");
        RequirementsText = Config.Bind(
            "Requirements Display Text",
            "Unlock Requirements Text",
            "Unlock Requirements");
        RequirementsSymbol = Config.Bind(
            "Requirements Display Text",
            "Unlock Requirements Symbol",
            "▼");
        IncompleteSymbol = Config.Bind(
            "Requirements Display Text",
            "Incomplete Symbol",
            "✖");
        FinishedSymbol = Config.Bind(
            "Requirements Display Text",
            "Finished Symbol",
            "✔"); 
        LevelText = Config.Bind(
            "Requirements Display Text",
            "Level",
            "Level {0}"); 
        QuestText = Config.Bind(
            "Requirements Display Text",
            "Quest",
            "Quest '{0}'");
        TransitText = Config.Bind(
            "Requirements Display Text",
            "Transit",
            "Transit to this map");

        GroundZeroText = Config.Bind(
            "Map Display Names",
            "Ground Zero",
            "Ground Zero");
        FactoryText = Config.Bind(
            "Map Display Names",
            "Factory",
            "Factory");
        CustomsText = Config.Bind(
            "Map Display Names",
            "Customs",
            "Customs");
        WoodsText = Config.Bind(
            "Map Display Names",
            "Woods",
            "Woods");
        ReserveText = Config.Bind(
            "Map Display Names",
            "Reserve",
            "Reserve");
        ShorelineText = Config.Bind(
            "Map Display Names",
            "Shoreline",
            "Shoreline");
        LighthouseText = Config.Bind(
            "Map Display Names",
            "Lighthouse",
            "Lighthouse");
        InterchangeText = Config.Bind(
            "Map Display Names",
            "Interchange",
            "Interchange");
        StreetsOfTarkovText = Config.Bind(
            "Map Display Names",
            "Streets of Tarkov",
            "Streets of Tarkov");
        TheLabText = Config.Bind(
            "Map Display Names",
            "The Lab",
            "The Lab");
        TheLabyrinthText = Config.Bind(
            "Map Display Names",
            "The Labyrinth",
            "The Labyrinth");
        TerminalText = Config.Bind(
            "Map Display Names",
            "Terminal",
            "Terminal");
        
        
        // MapProgressionManager.AddRequirements("Sandbox", new MapProgressionRequirements("", 0, false))
        //     .AddRequirements("Factory", new MapProgressionRequirements("Shooting Cans", 2, false))
        //     .AddRequirements("Customs", new MapProgressionRequirements("Debut", 4, false))
        //     .AddRequirements("Woods", new MapProgressionRequirements("Luxurious Life", 6, false))
        //     .AddRequirements("ReserveBase", new MapProgressionRequirements("Belka and Strelka", 8, false))
        //     .AddRequirements("Shoreline", new MapProgressionRequirements("The Bunker - Part 1", 10, false))
        //     .AddRequirements("Lighthouse", new MapProgressionRequirements("Chemical - Part 3", 12, false))
        //     .AddRequirements("Interchange", new MapProgressionRequirements("Only Business", 14, true))
        //     .AddRequirements("Streets of Tarkov", new MapProgressionRequirements("Population Census", 16, true))
        //     .AddRequirements("Laboratory", new MapProgressionRequirements("Beneath The Streets", 18, true))
        //     .AddRequirements("Labyrinth", new MapProgressionRequirements("Indisputable Authority", 20, true))
        //     .AddRequirements("Terminal", new MapProgressionRequirements("", 100, false)); // Impossible to unlock
        
        MapProgressionManager.AddRequirements("Sandbox", new MapProgressionRequirements("", 0, false))
            .AddRequirements("Factory", new MapProgressionRequirements("Shooting Cans", 2, false))
            .AddRequirements("Customs", new MapProgressionRequirements("Debut", 4, false))
            .AddRequirements("Woods", new MapProgressionRequirements("Luxurious Life", 6, false))
            .AddRequirements("ReserveBase", new MapProgressionRequirements("Belka and Strelka", 8, false))
            .AddRequirements("Shoreline", new MapProgressionRequirements("The Bunker - Part 1", 10, false))
            .AddRequirements("Lighthouse", new MapProgressionRequirements("Chemical - Part 3", 12, false))
            .AddRequirements("Interchange", new MapProgressionRequirements("Only Business", 14, true))
            .AddRequirements("Streets of Tarkov", new MapProgressionRequirements("Population Census", 16, true))
            .AddRequirements("Laboratory", new MapProgressionRequirements("Beneath The Streets", 18, true))
            .AddRequirements("Labyrinth", new MapProgressionRequirements("Indisputable Authority", 20, true))
            .AddRequirements("Terminal", new MapProgressionRequirements("", 100, false)); // Impossible to unlock
            
        MapProgressionRequirements nullRequirements = new MapProgressionRequirements("", 0, false);
        foreach (string map in MapProgressionManager.GetKeys())
        {
            MapProgressionRequirements currMapRequirements = MapProgressionManager.GetRequirementsOrDefault(map, nullRequirements);
            var transitEntry = Config.Bind(
                $"{map}",
                "Transit Requirement",
                currMapRequirements.Transit,
                $"Should a transit to {map} be required to access it?");
            var levelEntry = Config.Bind(
                $"{map}",
                "Level Requirement",
                currMapRequirements.Level,
                "The player level required to access this map.");
            var questEntry = Config.Bind(
                $"{map}",
                "Quest Requirement",
                currMapRequirements.Quest,
                "The quest that must be completed to access this map.");

            MapRequirements.Add(map, (questEntry, levelEntry, transitEntry));
        }
    }
}