using System;
using System.Linq;
using EFT.Quests;
using JetBrains.Annotations;
using SPT.Reflection.Utils;
using SPTMapProgression.MapProgression;
using SPTMapProgression.ModData;
using SPTMapProgression.Utility;

namespace SPTMapProgression.Helper;

public static class MapProgressionHelper
{
    public static bool IsLocationUnlocked(string locationName)
    {
        locationName = StringUtility.GetMapInternalName(locationName);
        MapProgressionRequirements mapRequirements =
            SptMapProgression.BepinConfig.MapProgressionManager.GetRequirements(locationName);
        if (mapRequirements == null) return true;
        int requiredLevel = mapRequirements.LevelConfigEntry.Value;
        string requiredQuest = mapRequirements.QuestIdConfigEntry.Value;
        bool transitRequired = mapRequirements.TransitConfigEntry.Value;
        if (requiredQuest.Length > 0 && !IsQuestCompleted(requiredQuest)) return false;
        if (requiredLevel > 0 && !IsLevelSufficient(locationName)) return false;
        if (transitRequired && !HasTransited(locationName)) return false;
        // Add Trader Check
        return true;
    }

    public static bool IsQuestCompleted(string questId)
    {
        var profile = ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile;
        var completed = profile.QuestsData.Where(q => q.Status == EQuestStatus.Success);
        foreach (var quest in completed)
        {
            if (quest.Template != null &&
                quest.Template.Id.Equals(questId,
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
    public static bool IsLevelSufficient(string locationName)
    { 
        locationName = StringUtility.GetMapInternalName(locationName);
        MapProgressionRequirements mapRequirements =
            SptMapProgression.BepinConfig.MapProgressionManager.GetRequirements(locationName);
        if (mapRequirements == null) return true;
        int requiredLevel = mapRequirements.LevelConfigEntry.Value;
        var profile = ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile;
        return profile.Info.Level + 1 > requiredLevel;
    }
    public static bool HasTransited(string locationName)
    {
        return ModSaveDataManager.Data.MapTransits.Contains(locationName);
    }
}