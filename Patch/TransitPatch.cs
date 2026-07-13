using System.Reflection;
using EFT;
using EFT.Interactive;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTMapProgression.ModData;
using SPTMapProgression.TarkovMap;
using SPTMapProgression.Utility;

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
        string transitLocation = TarkovMapClass.ToName(__instance.parameters.location);
        ModSaveDataManager.Data.MapTransits.Add(transitLocation);
        ModSaveDataManager.Save();

    }
    
}