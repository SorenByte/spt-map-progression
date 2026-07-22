using System.Collections.Generic;
using System.Reflection;
using EFT;
using EFT.Interactive;
using Fika.Core.Main.ClientClasses;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTMapProgression.ModData;
using SPTMapProgression.TarkovMap;

namespace SPTMapProgression.Patch;

public class FikaTransitPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(ClientTransitController), nameof(ClientTransitController.HandleClientExtract), [typeof(int), typeof(int)]);
    }
    
    [PatchPostfix]
    static void Postfix(ClientTransitController __instance, int transitId, int playerId)
    {
        if (!GClass1906.smethod_2(playerId, out var player))
        {
            SptMapProgression.LogSource.LogError("FikaTransitPatch::Postfix: Could not find player with id: " + playerId);
            return;
        }
        if (!__instance.Dictionary_0.TryGetValue(transitId, out var point))
        {
            SptMapProgression.LogSource.LogError("FikaTransitPatch::Postfix: Could not find transit point with id: " + transitId);
            return;
        }
        SptMapProgression.LogSource.LogDebug($"The client '{player.name}' has transited to {point.parameters.name}.");
        string transitLocation = TarkovMapClass.ToName(point.parameters.location);
        ModSaveDataManager.Data.MapTransits.Add(transitLocation);
        ModSaveDataManager.Save();
    }
    
    
}