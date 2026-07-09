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
        internal static MapProgressionManager MapProgressionManager;
        internal static ConfigEntry<bool> ShouldPlaySound;


        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");

            MapProgressionManager = new MapProgressionManager();
            
            MapProgressionManager.AddRequirements("Sandbox", new MapProgressionRequirements("", 0, false))
                .AddRequirements("Factory", new MapProgressionRequirements("Shooting Cans", 2, false))
                .AddRequirements("Customs", new MapProgressionRequirements("Debut", 4, false))
                .AddRequirements("Woods", new MapProgressionRequirements("Luxurious Life", 6, false))
                .AddRequirements("ReserveBase", new MapProgressionRequirements("Belka and Strelka", 8, false))
                .AddRequirements("Shoreline", new MapProgressionRequirements("The Bunker - Part 1", 10, false))
                .AddRequirements("Lighthouse", new MapProgressionRequirements("Friend From the West - Part 2", 12, false))
                .AddRequirements("Interchange", new MapProgressionRequirements("Only Business", 14, false))
                .AddRequirements("Streets of Tarkov", new MapProgressionRequirements("Population Census", 16, true))
                .AddRequirements("Laboratory", new MapProgressionRequirements("Beneath The Streets", 18, true))
                .AddRequirements("Labyrinth", new MapProgressionRequirements("Indisputable Authority", 20, true))
                .AddRequirements("Terminal", new MapProgressionRequirements("", 100, false)); // Impossible to unlock

            MapProgressionRequirements nullRequirements = new MapProgressionRequirements("", 0, false);

            ShouldPlaySound = Config.Bind(
                "General Settings",
                "Play Map Unlock Sound",
                true,
                "Whether or not to play the unlock sound when you view the map screen for the first time after unlocking a location");
            
            foreach (string map in MapProgressionManager.GetKeys())
            {
                MapProgressionRequirements currMapRequirements = MapProgressionManager.GetRequirementsOrDefault(map, nullRequirements);
                var transitEntry = Config.Bind(
                    $"{map}",
                    "Transit Requirement",
                    currMapRequirements.Transit,
                    $"Should a transit to {map} be required to access it?");
                var levelEntry = Config.Bind(
                    $"{map}",
                    "Level Requirement",
                    currMapRequirements.Level,
                    "The player level required to access this map.");
                var questEntry = Config.Bind(
                    $"{map}",
                    "Quest Requirement",
                    currMapRequirements.Quest,
                    "The quest that must be completed to access this map.");

                MapRequirements.Add(map, (questEntry, levelEntry, transitEntry));
            }
            
            new LocationButtonShowPatch().Enable();
            new TransitPatch().Enable();
        }

        private void OnDestroy()
        {
            ModSaveDataManager.Shutdown();
        }
    }
}
