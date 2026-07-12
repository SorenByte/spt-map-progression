using System.Collections.Generic;
using BepInEx.Configuration;
using EFT;
using SPTMapProgression.Config;
using SPTMapProgression.MapProgression;

namespace SPTMapProgression.Config;

public class BepinConfigDefault(ConfigFile config, MapProgressionManager mapProgressionManager) : BepinConfig
{
    internal readonly ConfigFile Config = config;
    internal readonly MapProgressionManager MapProgressionManager = mapProgressionManager;

    internal bool Initialized = false;
    
    public override void Init()
    {
        if (Initialized) return;
        Initialized = true;
        
        MapProgressionManager.AddRequirements("Sandbox", new MapProgressionRequirements("Sandbox", Config, "", "", 0, false))
            .AddRequirements("Factory", new MapProgressionRequirements("Factory", Config, "Shooting Cans", "657315df034d76585f032e01", 2, false))
            .AddRequirements("Customs", new MapProgressionRequirements("Customs", Config, "Debut", "5936d90786f7742b1420ba5b", 4, false))
            .AddRequirements("Woods", new MapProgressionRequirements("Woods", Config, "Luxurious Life", "657315e1dccd301f1301416a", 6, false))
            .AddRequirements("ReserveBase", new MapProgressionRequirements("ReserveBase", Config, "Belka and Strelka", "675c3507a06634b5110e3c18", 8, false))
            .AddRequirements("Shoreline", new MapProgressionRequirements("Shoreline", Config, "The Bunker - Part 1", "5ede55112c95834b583f052a", 10, false))
            .AddRequirements("Lighthouse", new MapProgressionRequirements("Lighthouse", Config, "Chemical - Part 3", "597a0e5786f77426d66c0636", 12, false))
            .AddRequirements("Interchange", new MapProgressionRequirements("Interchange", Config, "Only Business", "5ae448a386f7744d3730fff0", 14, true))
            .AddRequirements("Streets of Tarkov", new MapProgressionRequirements("Streets of Tarkov", Config, "Population Census", "639135d89444fb141f4e6eea", 16, true))
            .AddRequirements("Laboratory", new MapProgressionRequirements("Laboratory", Config, "Beneath The Streets", "66aba85403e0ee3101042877", 18, true))
            .AddRequirements("Labyrinth", new MapProgressionRequirements("Labyrinth", Config, "Indisputable Authority", "67a097379f2068e74603c6ac", 20, true))
            .AddRequirements("Terminal", new MapProgressionRequirements("Terminal", Config, "", "", 100, false)); // Impossible to unlock
        
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
        
    }
}