using BepInEx.Configuration;
using SPTMapProgression.MapProgression;

namespace SPTMapProgression.Config;

public class ClientConfigDefault(ConfigFile config) : BepinConfig
{
    public override void Init()
    {
        if (_initialized) return;
        _initialized = true;
        
        ShouldPlaySound = config.Bind(
        "General Settings",
        "Play Map Unlock Sound",
        true,
        "Whether or not to play the unlock sound when you view the map screen for the first time after unlocking a location");
            
        LockedText = config.Bind(
            "Requirements Display Text",
            "Location Locked",
            "{0} is Locked!");
        RequirementsText = config.Bind(
            "Requirements Display Text",
            "Unlock Requirements Text",
            "Unlock Requirements");
        RequirementsSymbol = config.Bind(
            "Requirements Display Text",
            "Unlock Requirements Symbol",
            "▼");
        IncompleteSymbol = config.Bind(
            "Requirements Display Text",
            "Incomplete Symbol",
            "✖");
        FinishedSymbol = config.Bind(
            "Requirements Display Text",
            "Finished Symbol",
            "✔"); 
        LevelText = config.Bind(
            "Requirements Display Text",
            "Level",
            "Level {0}"); 
        QuestText = config.Bind(
            "Requirements Display Text",
            "Quest",
            "Quest '{0}'");
        TransitText = config.Bind(
            "Requirements Display Text",
            "Transit",
            "Transit to this map");
        SurviveText = config.Bind(
            "Requirements Display Text",
            "Survive",
            "Survive {0}/{1} times on this map");

        GroundZeroText = config.Bind(
            "Map Display Names",
            "Ground Zero",
            "Ground Zero");
        FactoryText = config.Bind(
            "Map Display Names",
            "Factory",
            "Factory");
        CustomsText = config.Bind(
            "Map Display Names",
            "Customs",
            "Customs");
        WoodsText = config.Bind(
            "Map Display Names",
            "Woods",
            "Woods");
        ReserveText = config.Bind(
            "Map Display Names",
            "Reserve",
            "Reserve");
        ShorelineText = config.Bind(
            "Map Display Names",
            "Shoreline",
            "Shoreline");
        LighthouseText = config.Bind(
            "Map Display Names",
            "Lighthouse",
            "Lighthouse");
        InterchangeText = config.Bind(
            "Map Display Names",
            "Interchange",
            "Interchange");
        StreetsOfTarkovText = config.Bind(
            "Map Display Names",
            "Streets of Tarkov",
            "Streets of Tarkov");
        TheLabText = config.Bind(
            "Map Display Names",
            "The Lab",
            "The Lab");
        TheLabyrinthText = config.Bind(
            "Map Display Names",
            "The Labyrinth",
            "The Labyrinth");
        TerminalText = config.Bind(
            "Map Display Names",
            "Terminal",
            "Terminal");
        
    }
}