using BepInEx.Configuration;

namespace SPTMapProgression.MapProgression;

public class MapProgressionRequirements(
    MapProgressionManager manager,
    string map,
    string questName,
    string questId,
    int level,
    bool transit,
    int survive)
{
    internal readonly string Map = map;
    
    internal readonly ConfigEntry<string> QuestDisplayNameEntry = manager.Config.Bind(
        $"{manager.Side} - {map}",
        "Quest Requirement Display Name",
        questName,
        "The name that will be displayed.");
    
    internal readonly ConfigEntry<string> QuestIdConfigEntry = manager.Config.Bind(
        $"{manager.Side} - {map}",
        "Quest Requirement ID",
        questId,
        "The ID of the quest that must be completed to access this map.");
    
    internal readonly ConfigEntry<int> LevelConfigEntry = manager.Config.Bind(
        $"{manager.Side} - {map}",
        "Level Requirement",
        level,
        "The player level required to access this map.");
    
    internal readonly ConfigEntry<bool> TransitConfigEntry = manager.Config.Bind(
        $"{manager.Side} - {map}",
        "Transit Requirement",
        transit,
        $"Should a transit to {map} be required to access it?");
    
    internal readonly ConfigEntry<int> SurviveConfigEntry = manager.Config.Bind(
        $"{manager.Side} - {map}",
        "Survive Requirement",
        survive,
        $"How many times do you need to survive on {map} to access it?");
}