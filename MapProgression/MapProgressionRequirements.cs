using BepInEx.Configuration;

namespace SPTMapProgression.MapProgression;

public class MapProgressionRequirements
{

    internal ConfigEntry<string> QuestDisplayNameEntry;
    internal ConfigEntry<string> QuestIdConfigEntry;
    internal ConfigEntry<int> LevelConfigEntry;
    internal ConfigEntry<bool> TransitConfigEntry;
    // internal ConfigEntry<string> TraderNameConfigEntry;
    // internal ConfigEntry<int> TraderLoyaltyConfigEntry;
    
    public MapProgressionRequirements(string map, ConfigFile config, string questName, string questId, int level, bool transit)//, MapProgressionTraderRequirement trader)
    {
        TransitConfigEntry = config.Bind(
            $"{map}",
            "Transit Requirement",
            transit,
            $"Should a transit to {map} be required to access it?");
        LevelConfigEntry = config.Bind(
            $"{map}",
            "Level Requirement",
            level,
            "The player level required to access this map.");
        QuestIdConfigEntry = config.Bind(
            $"{map}",
            "Quest Requirement ID",
            questId,
            "The ID of the quest that must be completed to access this map.");
        QuestDisplayNameEntry = config.Bind(
            $"{map}",
            "Quest Requirement Display Name",
            questName,
            "The name that will be displayed.");
        // TraderNameConfigEntry = config.Bind(
        //     $"{map}",
        //     "Trader Requirement Name (SET LL TOO)",
        //     trader.Name,
        //     "The name of the trader to use for the loyalty check.");
        // TraderLoyaltyConfigEntry = config.Bind(
        //     $"{map}",
        //     "Trader Requirement Loyalty Level",
        //     trader.Loyalty,
        //     "The trader loyalty level required to access this map.");
    }
}