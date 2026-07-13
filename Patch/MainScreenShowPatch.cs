using System.Reflection;
using EFT;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTMapProgression.ModData;

namespace SPTMapProgression.Patch;

public class MainScreenShowPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(MenuScreen), nameof(MenuScreen.Show),
            [typeof(Profile), typeof(MatchmakerPlayerControllerClass), typeof(ESessionMode)]);
    }

    private static bool _initialized;
    
    [PatchPostfix]
    static void Postfix(Profile profile)
    {
        if (_initialized) return;
        _initialized = true;
        SptMapProgression.LogSource.LogDebug("Initializing ModSaveDataManager and BepinConfig");
        ModSaveDataManager.Init(profile);
        SptMapProgression.ClientConfig.Init();
    }
    
}