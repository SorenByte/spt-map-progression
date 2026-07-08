using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using EFT.Hideout;
using SPTMapProgression.Patch;
using System.Collections.Generic;

namespace SPTMapProgression
{
    [BepInPlugin("com.sorenbyte.SPTMapProgression", "sorenbyte-SPTMapProgression", "1.0.0")]
    public class SptMapProgression : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;
        internal static readonly Dictionary<string, (ConfigEntry<string> quest, ConfigEntry<int> level)> MapRequirements = new();


        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            // save the Logger to public static field so we can use it elsewhere in the project
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");

            (string quest, int level) defaultMapRequirement = ("", 0);
            foreach (string map in MapProgressionManager.DefaultRequirements.Keys)
            {
                (string quest, int level) currMapRequirements = MapProgressionManager.DefaultRequirements.GetValueOrDefault(map, defaultMapRequirement);
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

                // (ConfigEntry<string> quest, ConfigEntry<int> level) requirements = (questEntry, levelEntry);

                MapRequirements.Add(map, (questEntry, levelEntry)); //requirements);
            }
            
            new LocationButtonShowPatch().Enable();
        }
    }
}
