using System;
using System.Reflection;
using EFT;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTMapProgression.Config;
using SPTMapProgression.ModData;

namespace SPTMapProgression.Patch;

public class MainScreenShowPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(MenuScreen), nameof(MenuScreen.Show),
            new Type[] { typeof(Profile), typeof(MatchmakerPlayerControllerClass), typeof(ESessionMode) });
    }

    [PatchPostfix]
    static void Postfix(Profile profile)
    {
        SptMapProgression.LogSource.LogDebug("Initializing ModSaveDataManager and BepinConfig");
        ModSaveDataManager.Init(profile);
        SptMapProgression.BepinConfig.Init();
    }
    
}