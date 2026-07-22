using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using BepInEx.Logging;
using SPT.Reflection.Patching;
using SPTMapProgression.Compatibility;
using SPTMapProgression.Patch;
using SPTMapProgression.Config;
using SPTMapProgression.MapProgression;
using SPTMapProgression.ModData;

namespace SPTMapProgression
{
    [BepInPlugin("com.sorenbyte.SPTMapProgression", "sorenbyte-SPTMapProgression", "1.5.1")]
    [BepInDependency("com.fika.core", BepInDependency.DependencyFlags.SoftDependency)]
    public class SptMapProgression : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;
        internal static MapProgressionManager PmcMapProgressionManager;
        internal static MapProgressionManager ScavMapProgressionManager;
        internal static ClientConfigDefault ClientConfig;
        private static ConfigFile _configFile;

        private static MapProgressionPatchManager _patchManager;
        private static bool _modDisabled;
        private static int _updateRate = 60;
        private static int _updateTicker;
        
        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");
            _configFile = Config;

            _patchManager = new MapProgressionPatchManager(LogSource);
            _patchManager.AddPatch((new MainScreenShowPatch()));
            _patchManager.AddPatch((new LocationButtonShowPatch()));
            _patchManager.AddPatch((new TransitPatch()));
            _patchManager.AddPatch((new LocationScreenShowPatch()));
            _patchManager.AddPatch((new ExtractSurvivePatch()));
            _patchManager.EnablePatches();
            
            ClientConfig = new ClientConfigDefault(_configFile);
            
            PmcMapProgressionManager = new MapProgressionManager(_configFile, "PMC");
            MapProgressionHelper.SetPmcRequirements(PmcMapProgressionManager);
            
            ScavMapProgressionManager = new MapProgressionManager(_configFile, "Scav");
            MapProgressionHelper.SetScavRequirements(ScavMapProgressionManager);
            
            ModCompatibilityManager.Init(LogSource, _patchManager);
        }

        private void FixedUpdate()
        {
            _updateTicker++;
            if (_updateTicker < _updateRate) return; 
            _updateTicker = 0;
            
            if (ClientConfig == null || !ClientConfig._initialized) return;
            
            if (!_modDisabled && !ClientConfig.EnableMod.Value)
            {
                _modDisabled = true;
                _patchManager.DisablePatches();
            }
            else if (_modDisabled)
            {
                _modDisabled = false;
                _patchManager.EnablePatches();
            }
        }
        
        private void OnDestroy()
        {
            ModSaveDataManager.Shutdown();
        }
    }
}
