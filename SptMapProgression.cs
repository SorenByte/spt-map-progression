using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
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
        private static ConfigFile ConfigFile;
        
        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            LogSource = Logger;
            LogSource.LogInfo("plugin loaded!");
            ConfigFile = Config;
            
            ClientConfig = new ClientConfigDefault(ConfigFile);
            
            PmcMapProgressionManager = new MapProgressionManager(ConfigFile, "PMC");
            MapProgressionHelper.SetPmcRequirements(PmcMapProgressionManager);
            
            ScavMapProgressionManager = new MapProgressionManager(ConfigFile, "Scav");
            MapProgressionHelper.SetScavRequirements(ScavMapProgressionManager);
            
            new MainScreenShowPatch().Enable();
            new LocationButtonShowPatch().Enable();
            new TransitPatch().Enable();
            new LocationScreenShowPatch().Enable();
            new ExtractSurvivePatch().Enable();
        }

        private void OnDestroy()
        {
            ModSaveDataManager.Shutdown();
        }
    }
}
