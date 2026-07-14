using System.Reflection;
using BepInEx.Logging;
using EFT;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTMapProgression.MapProgression;
using SPTMapProgression.ModData;
using SPTMapProgression.Utility;

namespace SPTMapProgression.Patch;

public class MainScreenShowPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(MenuScreen), nameof(MenuScreen.Show),
            [typeof(Profile), typeof(MatchmakerPlayerControllerClass), typeof(ESessionMode)]);
    }

    private static bool _initialized;
    private static ManualLogSource LogSource => SptMapProgression.LogSource;
    
    [PatchPostfix]
    static void Postfix(Profile profile)
    {
        LogSource.LogDebug($"Current equipment value: {MathUtility.GetFormattedNumber(MapProgressionHelper.GetEquipmentValue(profile))}");
        if (_initialized) return;
        _initialized = true;
        LogSource.LogDebug("Initializing ModSaveDataManager and BepinConfig");
        ModSaveDataManager.Init(profile);
        SptMapProgression.ClientConfig.Init();
    }
    
}