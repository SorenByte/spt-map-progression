using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using EFT.Hideout;
using SPTMapProgression.Patch;
using System.Collections.Generic;
using SPTMapProgression.Config;
using SPTMapProgression.MapProgression;
using SPTMapProgression.ModData;

namespace SPTMapProgression
{
    [BepInPlugin("com.sorenbyte.SPTMapProgression", "sorenbyte-SPTMapProgression", "1.0.0")]
    public class SptMapProgression : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;
        internal static MapProgressionManager MapProgressionManager;
        internal static BepinConfigDefault BepinConfig;
        
        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");
            MapProgressionManager = new MapProgressionManager();
            BepinConfig = new BepinConfigDefault(Config, MapProgressionManager);
            BepinConfig.Init();
            
            new LocationButtonShowPatch().Enable();
            new TransitPatch().Enable();
        }

        private void OnDestroy()
        {
            ModSaveDataManager.Shutdown();
        }
    }
}
