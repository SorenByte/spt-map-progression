using System;
using System.Collections.Generic;
using System.Linq;
using EFT.Quests;
using SPT.Reflection.Utils;
using SPTMapProgression.ModData;

namespace SPTMapProgression.MapProgression
{
    internal static class MapProgressionManager
    {
        internal static readonly Dictionary<string, (string quest, int level, bool transit)> DefaultRequirements = new()
        {
            { "Sandbox", ("", 0, false) },
            { "Factory", ("Shooting Cans", 2, false) },
            { "Customs", ("Debut", 4, false) },
            { "Woods", ("Luxurious Life", 6, false) },
            { "ReserveBase", ("Belka and Strelka", 8, false) },
            { "Shoreline", ("The Bunker - Part 1", 10, false) },
            { "Lighthouse", ("Friend From the West - Part 2", 12, false) },
            { "Interchange", ("Only Business", 14, false) },
            { "Streets of Tarkov", ("Population Census", 16, true) },
            { "Laboratory", ("Beneath The Streets", 18, true) },
            { "Labyrinth", ("Indisputable Authority", 20, true)},
            { "Terminal", ("", 100, false) }
        };

        internal static bool IsLocationUnlocked(string locationName)
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

        internal static bool IsQuestCompleted(string questName)
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
        internal static bool IsLevelSufficient(string locationName)
        { 
            if (!SptMapProgression.MapRequirements.TryGetValue(locationName, out var mapRequirements)) return false;
            int requiredLevel = mapRequirements.level.Value;
            var profile = ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile;
            return profile.Info.Level + 1 > requiredLevel;
        }
        internal static bool HasTransited(string locationName)
        {
            return ModSaveDataManager.Data.MapTransits.Contains(locationName);
        }
        
    }
}