using System.Collections.Generic;
using BepInEx.Configuration;

namespace SPTMapProgression.Config;

public abstract class BepinConfig()
{
    
    // Collections
    public Dictionary<string, (ConfigEntry<string> quest, ConfigEntry<int> level, ConfigEntry<bool> transit)> MapRequirements { get; internal set; }
    
    // Bool
    public ConfigEntry<bool> ShouldPlaySound { get; internal set; }
    
    // Requirement texts
    public ConfigEntry<string> LockedText { get; internal set; }
    public ConfigEntry<string> RequirementsText { get; internal set; }
    public ConfigEntry<string> RequirementsSymbol { get; internal set; }
    public ConfigEntry<string> IncompleteSymbol { get; internal set; }
    public ConfigEntry<string> FinishedSymbol { get; internal set; }
    public ConfigEntry<string> LevelText { get; internal set; }
    public ConfigEntry<string> QuestText { get; internal set; }
    public ConfigEntry<string> TransitText { get; internal set; }
    
    // Map display names
    public ConfigEntry<string> GroundZeroText { get; internal set; }
    public ConfigEntry<string> FactoryText { get; internal set; }
    public ConfigEntry<string> CustomsText { get; internal set; }
    public ConfigEntry<string> WoodsText { get; internal set; }
    public ConfigEntry<string> ReserveText { get; internal set; }
    public ConfigEntry<string> ShorelineText { get; internal set; }
    public ConfigEntry<string> LighthouseText { get; internal set; }
    public ConfigEntry<string> InterchangeText { get; internal set; }
    public ConfigEntry<string> StreetsOfTarkovText { get; internal set; }
    public ConfigEntry<string> TheLabText { get; internal set; }
    public ConfigEntry<string> TheLabyrinthText { get; internal set; }
    public ConfigEntry<string> TerminalText { get; internal set; }
    
    public abstract void Init();

}