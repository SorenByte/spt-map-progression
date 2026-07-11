using System;
using System.Reflection;
using EFT;
using EFT.Interactive;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTMapProgression.ModData;
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
        // string transitLocation = __instance.parameters.location;
        string transitLocation = StringUtility.GetMapInternalName(__instance.parameters.location);
        // if (transitLocation.Equals("TarkovStreets", StringComparison.OrdinalIgnoreCase))
        //     transitLocation = "Streets of Tarkov";
        // if (transitLocation.Equals("factory4_day", StringComparison.OrdinalIgnoreCase))
        //     transitLocation = "Factory";
        // if (transitLocation.Equals("factory4_night", StringComparison.OrdinalIgnoreCase))
        //     transitLocation = "Factory";
        // if (transitLocation.Equals("bigmap", StringComparison.OrdinalIgnoreCase))
        //     transitLocation = "Customs";
        // if (transitLocation.Equals("RezervBase", StringComparison.OrdinalIgnoreCase))
        //     transitLocation = "ReserveBase";
        // if (transitLocation.Equals("laboratory", StringComparison.OrdinalIgnoreCase))
        //     transitLocation = "Laboratory";
        // if (transitLocation.Equals("Sandbox_high", StringComparison.OrdinalIgnoreCase))
        //     transitLocation = "Sandbox";
        ModSaveDataManager.Data.MapTransits.Add(transitLocation);
        ModSaveDataManager.Save();

    }
    
}