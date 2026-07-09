using System.Reflection;
using EFT;
using EFT.Interactive;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTMapProgression.ModData;

namespace SPTMapProgression.Patch;

public class TransitPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(TransitPoint),
            nameof(TransitPoint.GroupEnter));
    }

    [PatchPostfix]
    public static void Postfix(TransitPoint __instance, Player player)
    {
        // SptMapProgression.LogSource.LogDebug($"Transit heard");
        // SptMapProgression.LogSource.LogDebug($"Player transited from {__instance.parameters.location}");
        string transitLocation = __instance.parameters.location;
        ModSaveDataManager.Data.MapTransits.Add(transitLocation);

    }
    
}