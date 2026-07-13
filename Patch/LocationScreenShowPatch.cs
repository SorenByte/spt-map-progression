using System.Reflection;
using EFT;
using EFT.HealthSystem;
using EFT.InventoryLogic;
using EFT.UI.Matchmaker;
using HarmonyLib;
using SPT.Reflection.Patching;
using UnityEngine.UI;

namespace SPTMapProgression.Patch;

public class LocationScreenShowPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(MatchMakerSelectionLocationScreen), nameof(MatchMakerSelectionLocationScreen.Show), [typeof(ISession), typeof(RaidSettings), typeof(MatchmakerPlayerControllerClass)]);
    }

    internal static RaidSettings CurrentRaidSettings;
    
    [PatchPrefix]
    private static void Prefix(MatchMakerSelectionLocationScreen __instance, RaidSettings raidSettings)
    {
        CurrentRaidSettings = raidSettings;
    }
}