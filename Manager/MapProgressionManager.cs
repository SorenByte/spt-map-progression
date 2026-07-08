using BepInEx.Configuration;
using EFT.Quests;
using SPT.Reflection.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPTMapProgression
{
    internal static class MapProgressionManager
    {
        internal static readonly Dictionary<string, (string quest, int level)> DefaultRequirements = new()
        {
            { "Sandbox", ("", 0) },
            { "Customs", ("", 2) },
            { "Factory", ("", 4) },
            { "Woods", ("", 6) },
            { "ReserveBase", ("", 8) },
            { "Shoreline", ("", 10) },
            { "Lighthouse", ("", 12) },
            { "Interchange", ("", 14) },
            { "Streets of Tarkov", ("", 16) },
            { "Laboratory", ("", 18) },
            { "Terminal", ("", 80) }
        };

        internal static bool IsLocationUnlocked(string locationName)
        {
            if (!SptMapProgression.MapRequirements.TryGetValue(locationName, out var mapRequirements)) return false;
            var profile = ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile;
            int requiredLevel = mapRequirements.level.Value;
            string requiredQuest = mapRequirements.quest.Value;
            // SPTMapProgression.LogSource.LogDebug($"Checking if location '{locationName}' is unlocked. Player level: {profile.Info.Level}, Required level: {requiredLevel}, Required Quest: {requiredQuest}");
            if (requiredQuest.Length > 0 && !IsQuestCompleted(requiredQuest)) return false;
            if (requiredLevel > 0 && profile.Info.Level < requiredLevel) return false;
            return true;
        }

        private static bool IsQuestCompleted(string questName)
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
    }
}