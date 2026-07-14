using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using SPT.Reflection.Patching;
using SPTMapProgression.Patch;
using SPTMapProgression.Config;
using SPTMapProgression.MapProgression;
using SPTMapProgression.ModData;

namespace SPTMapProgression
{
    [BepInPlugin("com.sorenbyte.SPTMapProgression", "sorenbyte-SPTMapProgression", "1.0.0")]
    public class SptMapProgression : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;
        internal static MapProgressionManager PmcMapProgressionManager;
        internal static MapProgressionManager ScavMapProgressionManager;
        internal static ClientConfigDefault ClientConfig;
        private static ConfigFile _configFile;

        private static List<ModulePatch> _patches;
        private static bool _modDisabled;
        private static int _updateRate = 60;
        private static int _updateTicker = 0;
        
        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");
            _configFile = Config;
            
            ClientConfig = new ClientConfigDefault(_configFile);
            
            PmcMapProgressionManager = new MapProgressionManager(_configFile, "PMC");
            MapProgressionHelper.SetPmcRequirements(PmcMapProgressionManager);
            
            ScavMapProgressionManager = new MapProgressionManager(_configFile, "Scav");
            MapProgressionHelper.SetScavRequirements(ScavMapProgressionManager);

            _patches = [];
            _patches.Add(new MainScreenShowPatch());
            _patches.Add(new LocationButtonShowPatch());
            _patches.Add(new TransitPatch());
            _patches.Add(new LocationScreenShowPatch());
            _patches.Add(new ExtractSurvivePatch());
            EnablePatches();
        }

        private void FixedUpdate()
        {
            _updateTicker++;
            if (_updateTicker < _updateRate) return; 
            _updateTicker = 0;
            
            if (ClientConfig == null || !ClientConfig._initialized) return;
            
            if (!ClientConfig.EnableMod.Value)
            {
                if (_modDisabled) return;
                _modDisabled = true;
                DisablePatches();
            }
            else
            {
                if (!_modDisabled) return;
                _modDisabled = false;
                EnablePatches();
            }
        }
        private void EnablePatches()
        {
            foreach (ModulePatch p in _patches)
            {
                p.Enable();
            }
        }
        
        private void DisablePatches()
        {
            foreach (ModulePatch p in _patches)
            {
                p.Disable();
            }
        }
        
        private void OnDestroy()
        {
            ModSaveDataManager.Shutdown();
        }
    }
}
