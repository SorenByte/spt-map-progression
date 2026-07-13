using System.Reflection;
using EFT;
using EFT.HealthSystem;
using EFT.InventoryLogic;
using EFT.UI.Matchmaker;
using HarmonyLib;
using SPT.Reflection.Patching;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SPTMapProgression.Patch;

public class SideSelectPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(MatchMakerSideSelectionScreen), nameof(MatchMakerSideSelectionScreen.Show), [typeof(ISession), typeof(RaidSettings), typeof(IHealthController), typeof(InventoryController)]);
    }

    // public static bool IsScav;
    
    [PatchPrefix]
    private static void Prefix(MatchMakerSideSelectionScreen __instance, RaidSettings raidSettings, Button ____savagesBigButton)
    {
        SptMapProgression.LogSource.LogDebug($"Is Scav: {raidSettings.IsScav}");
        // ____savagesBigButton.onClick.AddListener((UnityAction) (OnScavButtonClicked));
        // IsScav = side.Equals(ESideType.Savage);
    }

    private static void OnScavButtonClicked()
    {
        SptMapProgression.LogSource.LogDebug("Scav button was clicked.");
    }
    
}