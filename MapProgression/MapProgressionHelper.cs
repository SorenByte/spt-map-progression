using System;
using System.Collections.Generic;
using System.Linq;
using EFT.Quests;
using SPT.Reflection.Utils;
using SPTMapProgression.ModData;
using SPTMapProgression.TarkovMap;
using SPTMapProgression.Utility;

namespace SPTMapProgression.MapProgression;

public static class MapProgressionHelper
{
    public static bool IsLocationUnlockedForSide(string locationName, bool scavSide)
    {
        locationName = TarkovMapClass.ToName(locationName);
        MapProgressionRequirements mapRequirements;
        if (scavSide)
        {
            mapRequirements = SptMapProgression.ScavMapProgressionManager.GetRequirements(locationName);
        }
        else
        {
            mapRequirements = SptMapProgression.PmcMapProgressionManager.GetRequirements(locationName);
        }
        if (mapRequirements == null) return true;
        int requiredLevel = mapRequirements.LevelConfigEntry.Value;
        string requiredQuest = mapRequirements.QuestIdConfigEntry.Value;
        bool transitRequired = mapRequirements.TransitConfigEntry.Value;
        int requiredSurvives = mapRequirements.SurviveConfigEntry.Value;
        SptMapProgression.LogSource.LogDebug($"Location: {locationName}");        
        if (requiredQuest.Length > 0 && !IsQuestCompleted(requiredQuest)) return false;
        SptMapProgression.LogSource.LogDebug("Quest satisfied");
        if (requiredLevel > 0 && !IsLevelSufficient(requiredLevel)) return false;
        SptMapProgression.LogSource.LogDebug("Level satisfied");
        if (transitRequired && !HasTransited(locationName)) return false;
        SptMapProgression.LogSource.LogDebug("Transit satisfied");
        if (requiredSurvives > 0 && !IsSurvivesSufficient(locationName, requiredSurvives)) return false;
        SptMapProgression.LogSource.LogDebug("Survives satisfied");
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
    public static bool IsLevelSufficient(int requiredLevel)
    { 
        var profile = ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile;
        return profile.Info.Level + 1 > requiredLevel;
    }
    public static bool IsSurvivesSufficient(string map, int requiredSurvives)
    {
        int survives = GetMapSurvives(TarkovMapClass.ToName(map));
        return survives >= requiredSurvives;
    }

    public static int GetMapSurvives(string map)
    {
        return ModSaveDataManager.Data.MapSurvives.GetValueOrDefault(TarkovMapClass.ToName(map), 0);
    }
    public static bool HasTransited(string locationName)
    {
        return ModSaveDataManager.Data.MapTransits.Contains(locationName);
    }

    internal static void SetScavRequirements(MapProgressionManager manager)
    {
        manager
            .AddRequirements(new MapProgressionRequirements(manager, "Sandbox", "", "", 0, false, 3))
            .AddRequirements(new MapProgressionRequirements(manager,  "Factory", "", "", 0, false, 3))
            .AddRequirements(new MapProgressionRequirements(manager,  "Customs", "", "", 0, false, 3))
            .AddRequirements(new MapProgressionRequirements(manager,  "Woods", "", "", 0, false, 4))
            .AddRequirements(new MapProgressionRequirements(manager,  "ReserveBase", "", "", 0, false, 4))
            .AddRequirements(new MapProgressionRequirements(manager,  "Shoreline", "", "", 0, false, 4))
            .AddRequirements(new MapProgressionRequirements(manager,  "Lighthouse", "", "", 0, false, 5))
            .AddRequirements(new MapProgressionRequirements(manager,  "Interchange", "", "", 0, false, 5))
            .AddRequirements(new MapProgressionRequirements(manager, "Streets of Tarkov", "", "", 0, false, 5))
            .AddRequirements(new MapProgressionRequirements(manager, "Laboratory", "", "", 0, false, 10))
            .AddRequirements(new MapProgressionRequirements(manager, "Labyrinth", "", "", 0, false, 10))
            .AddRequirements(new MapProgressionRequirements(manager, "Terminal", "", "", 100, false, 0)); // Impossible to unlock
    }

    internal static void SetPmcRequirements(MapProgressionManager manager)
    {
        manager
            .AddRequirements(new MapProgressionRequirements(manager, "Sandbox", "", "", 0, false, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Factory", "Shooting Cans", "657315df034d76585f032e01", 2, false, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Customs", "Debut", "5936d90786f7742b1420ba5b", 4, false, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Woods", "Luxurious Life", "657315e1dccd301f1301416a", 6, false, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "ReserveBase", "Belka and Strelka", "675c3507a06634b5110e3c18", 8, false, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Shoreline", "The Bunker - Part 1", "5ede55112c95834b583f052a", 10, false, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Lighthouse", "Chemical - Part 3", "597a0e5786f77426d66c0636", 12, false, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Interchange", "Only Business", "5ae448a386f7744d3730fff0", 14, true, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Streets of Tarkov", "Population Census", "639135d89444fb141f4e6eea", 16, true, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Laboratory", "Beneath The Streets", "66aba85403e0ee3101042877", 18, true, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Labyrinth", "Indisputable Authority", "67a097379f2068e74603c6ac", 20, true, 0))
            .AddRequirements(new MapProgressionRequirements(manager, "Terminal", "", "", 100, false, 0)); // Impossible to unlock
    }
    
}