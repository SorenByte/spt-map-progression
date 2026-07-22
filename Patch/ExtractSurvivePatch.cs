using System.Collections.Generic;
using System.Reflection;
using EFT;
using EFT.UI.Map;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTMapProgression.ModData;

namespace SPTMapProgression.Patch;

public class ExtractSurvivePatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(Class308), nameof(Class308.LocalRaidEnded));
    }

    [PatchPostfix]
    static void Postfix(ISession __instance, LocalRaidSettings settings, RaidEndDescriptorClass results, FlatItemsDataClass[] lostInsuredItems, Dictionary<string, FlatItemsDataClass[]> transferItems)
    {
        if (results.result != ExitStatus.Survived) return;
        int currSurvives = ModSaveDataManager.Data.MapSurvives.GetValueOrDefault(settings.selectedLocation.Name, 0);
        currSurvives += 1;
        ModSaveDataManager.Data.MapSurvives[settings.selectedLocation.Name] = currSurvives;
        ModSaveDataManager.Save();
    }
    
}