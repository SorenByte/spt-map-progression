using System;
using System.Collections.Generic;
using BepInEx.Logging;
using SPT.Reflection.Patching;

namespace SPTMapProgression.Patch;

public class MapProgressionPatchManager(ManualLogSource logSource)
{
    private readonly ManualLogSource _logSource = logSource;
    
    private readonly List<ModulePatch> _patches = [];
    
    public void EnablePatches()
    {
        foreach (ModulePatch p in _patches)
        {
            if (p.IsActive) continue;
            try
            {
                p.Enable();
            }
            catch (Exception e)
            { 
                _logSource.LogError($"A patch failed to enable! Error Message: '{e.Message}'");
            }
        }
    }

    public void DisablePatches()
    {
        foreach (ModulePatch p in _patches)
        {
            if (!p.IsActive) continue;
            p.Disable();
        }
    }
    
    public void AddPatch(ModulePatch patch, bool enableImmediately = false)
    {
        _patches.Add(patch);
        if (enableImmediately) patch.Enable();
    }
    
    public List<ModulePatch> GetPatches()
    {
        return _patches;
    }
    
}