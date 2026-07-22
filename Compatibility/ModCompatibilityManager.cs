using System;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using SPTMapProgression.Patch;

namespace SPTMapProgression.Compatibility;

public static class ModCompatibilityManager
{
    private static ManualLogSource LogSource;
    private static MapProgressionPatchManager _patchManager;
    
    public static bool IsFikaDetected = false;
    public static FikaCompatibility FikaCompatibility;
    
    public static void Init(ManualLogSource logSource, MapProgressionPatchManager patchManager)
    {
        LogSource = logSource;
        _patchManager = patchManager;
        SetActiveMods();
        InitCompatibility();
    }

    private static void SetActiveMods()
    {
        if (Chainloader.PluginInfos.ContainsKey("com.fika.core")) IsFikaDetected = true;
    }
    
    private static void InitCompatibility()
    {
        if (IsFikaDetected)
        {
            LogSource.LogInfo("Fika was detected. Adding patches..");
            try { _patchManager.AddPatch(new FikaTransitPatch(), true); } 
            catch (Exception e) { LogSource.LogError($"Failed to init FikaTransitPatch: {e.Message}"); }
            FikaCompatibility = new FikaCompatibility();
        }
    }

}