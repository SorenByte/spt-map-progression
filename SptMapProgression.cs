using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using EFT.Hideout;
using SPTMapProgression.Patch;
using System.Collections.Generic;
using SPTMapProgression.MapProgression;
using SPTMapProgression.ModData;

namespace SPTMapProgression
{
    [BepInPlugin("com.sorenbyte.SPTMapProgression", "sorenbyte-SPTMapProgression", "1.0.0")]
    public class SptMapProgression : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;
        internal static readonly Dictionary<string, (ConfigEntry<string> quest, ConfigEntry<int> level, ConfigEntry<bool> transit)> MapRequirements = new();


        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            // save the Logger to public static field so we can use it elsewhere in the project
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");

            ModSaveDataManager.Load();
            
            (string quest, int level, bool transit) defaultMapRequirement = ("", 0, false);
            foreach (string map in MapProgressionManager.DefaultRequirements.Keys)
            {
                (string quest, int level, bool transit) currMapRequirements = MapProgressionManager.DefaultRequirements.GetValueOrDefault(map, defaultMapRequirement);
                var levelEntry = Config.Bind(
                    $"{map}",
                    "Level Requirement",
                    currMapRequirements.level,
                    "The player level required to access this map.");
                var questEntry = Config.Bind(
                    $"{map}",
                    "Quest Requirement",
                    currMapRequirements.quest,
                    "The quest that must be completed to access this map.");
                var transitEntry = Config.Bind(
                    $"{map}",
                    "Transit Requirement",
                    currMapRequirements.transit,
                    $"Should a transit to {map} be required to access it?");

                MapRequirements.Add(map, (questEntry, levelEntry, transitEntry));
            }
            
            new LocationButtonShowPatch().Enable();
            new TransitPatch().Enable();
        }

        private void OnDestroy()
        {
            ModSaveDataManager.Save();
        }
    }
}
