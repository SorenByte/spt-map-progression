using BepInEx.Configuration;
using SPTMapProgression.MapProgression;

namespace SPTMapProgression.Config;

public class ClientConfigDefault(ConfigFile config) : BepinConfig
{
    public override void Init()
    {
        if (_initialized) return;
        _initialized = true;

        string section;

        section = "Global Toggles";
        EnableMod = config.Bind(
            section,
            "Enable Mod",
            true,
            "Setting this to false completely disables every feature of the mod.");
        EnablePmcRequirements = config.Bind(
            section,
            "Enable PMC Requirements",
            true,
            "Whether or not your PMC can have any map requirements.");
        EnableScavRequirements = config.Bind(
            section,
            "Enable PMC Requirements",
            true,
            "Whether or not your Scav can have any map requirements.");
        EnableLevelRequirement = config.Bind(
            section,
            "Enable Level Requirement",
            true,
            "Whether or not maps can have a level requirement.");
        EnableQuestRequirement = config.Bind(
            section,
            "Enable Quest Requirement",
            true,
            "Whether or not maps can have a quest requirement.");
        EnableTransitRequirement = config.Bind(
            section,
            "Enable Transit Requirement",
            true,
            "Whether or not maps can have a transit requirement.");
        EnableSurviveRequirement = config.Bind(
            section,
            "Enable Survive Requirement",
            true,
            "Whether or not maps can have a survive requirement.");
        EnableEquipmentValueRequirement = config.Bind(
            section,
            "Enable Equipment Value Requirement",
            false,
            "Whether or not maps can have an equipment value requirement.");
        ShouldPlaySound = config.Bind(
            section,
        "Play Map Unlock Sound",
        true,
        "Whether or not to play the unlock sound when you view the map screen for the first time after unlocking a location");
        
        section = "Requirements Display Text";
        LockedText = config.Bind(
            section,
            "Location Locked",
            "{0} is Locked!");
        RequirementsText = config.Bind(
            section,
            "Unlock Requirements Text",
            "Unlock Requirements");
        RequirementsSymbol = config.Bind(
            section,
            "Unlock Requirements Symbol",
            "▼");
        IncompleteSymbol = config.Bind(
            section,
            "Incomplete Symbol",
            "✖");
        FinishedSymbol = config.Bind(
            section,
            "Finished Symbol",
            "✔"); 
        LevelText = config.Bind(
            section,
            "Level",
            "Level {0}"); 
        QuestText = config.Bind(
            section,
            "Quest",
            "Quest '{0}'");
        TransitText = config.Bind(
            section,
            "Transit",
            "Transit to this map");
        SurviveText = config.Bind(
            section,
            "Survive",
            "Survive {0}/{1} times on this map");
        EquipmentValueText = config.Bind(
            section,
            "Equipment Value",
            "{0}/{1} Equipment Value");

        section = "Map Display Names";
        GroundZeroText = config.Bind(
            section,
            "Ground Zero",
            "Ground Zero");
        FactoryText = config.Bind(
            section,
            "Factory",
            "Factory");
        CustomsText = config.Bind(
            section,
            "Customs",
            "Customs");
        WoodsText = config.Bind(
            section,
            "Woods",
            "Woods");
        ReserveText = config.Bind(
            section,
            "Reserve",
            "Reserve");
        ShorelineText = config.Bind(
            section,
            "Shoreline",
            "Shoreline");
        LighthouseText = config.Bind(
            section,
            "Lighthouse",
            "Lighthouse");
        InterchangeText = config.Bind(
            section,
            "Interchange",
            "Interchange");
        StreetsOfTarkovText = config.Bind(
            section,
            "Streets of Tarkov",
            "Streets of Tarkov");
        TheLabText = config.Bind(
            section,
            "The Lab",
            "The Lab");
        TheLabyrinthText = config.Bind(
            section,
            "The Labyrinth",
            "The Labyrinth");
        TerminalText = config.Bind(
            section,
            "Terminal",
            "Terminal");
        
    }
}