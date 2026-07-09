using System;
using System.Linq;
using EFT.Quests;
using SPT.Reflection.Utils;
using SPTMapProgression.ModData;

namespace SPTMapProgression.Helper;

public static class MapProgressionHelper
{
    public static bool IsLocationUnlocked(string locationName)
    {
        if (!SptMapProgression.MapRequirements.TryGetValue(locationName, out var mapRequirements)) return false;
        var profile = ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile;
        int requiredLevel = mapRequirements.level.Value;
        string requiredQuest = mapRequirements.quest.Value;
        bool transitRequired = mapRequirements.transit.Value;
        if (requiredQuest.Length > 0 && !IsQuestCompleted(requiredQuest)) return false;
        if (requiredLevel > 0 && !IsLevelSufficient(locationName)) return false;
        if (transitRequired && !HasTransited(locationName)) return false;
        return true;
    }

    public static bool IsQuestCompleted(string questName)
    {
        var profile = ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile;
        var completed = profile.QuestsData.Where(q => q.Status == EQuestStatus.Success);
        foreach (var quest in completed)
        {
            if (quest.Template != null &&
                quest.Template.Name.Equals(questName,
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }
    public static bool IsLevelSufficient(string locationName)
    { 
        if (!SptMapProgression.MapRequirements.TryGetValue(locationName, out var mapRequirements)) return false;
        int requiredLevel = mapRequirements.level.Value;
        var profile = ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile;
        return profile.Info.Level + 1 > requiredLevel;
    }
    public static bool HasTransited(string locationName)
    {
        return ModSaveDataManager.Data.MapTransits.Contains(locationName);
    }
}