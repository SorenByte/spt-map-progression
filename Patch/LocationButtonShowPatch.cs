using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.UI;

namespace SPTMapProgression.Patch
{
    internal class LocationButtonShowPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(LocationButton), nameof(LocationButton.Show));
        }

        private static void LockMap(LocationButton locationButton, LocationSettingsClass.Location location, UISpawnableToggle spawnableToggle, GameObject lockedIcon, GameObject bossIcon, Image iconImage, GameObject infoPanel, CustomTextMeshProUGUI infoText)
        {
            spawnableToggle.HideGameObject();
            lockedIcon.SetActive(true);
            var lockedIconGraphic = lockedIcon.GetComponent<Image>();
            // Allow the HoverTooltipArea to show text
            if (lockedIconGraphic != null) lockedIconGraphic.raycastTarget = true;
            bossIcon.SetActive(false);
            iconImage.enabled = false;
            infoPanel.SetActive(false);
            var shadow = locationButton.transform.Find("Shadow");
            if (shadow != null) shadow.gameObject.SetActive(false);
            var background = locationButton.transform.Find("Background");
            if (background != null) background.gameObject.SetActive(false);
            // SPTMapProgression.LogSource.LogDebug($"ItemUiContext.Instance null? {ItemUiContext.Instance == null}");
            HoverTooltipArea hoverTooltipArea = lockedIcon.GetOrAddComponent<HoverTooltipArea>();
            string locationName = location.Name;
            if (locationName.Equals("ReserveBase", StringComparison.OrdinalIgnoreCase)) locationName = "Reserve";
            if (locationName.Equals("Laboratory", StringComparison.OrdinalIgnoreCase)) locationName = "The Lab";
            if (locationName.Equals("Sandbox", StringComparison.OrdinalIgnoreCase)) locationName = "Ground Zero";
            (ConfigEntry<string> quest, ConfigEntry<int> level) requirements = SptMapProgression.MapRequirements[location.Name];
            string questText = requirements.quest.Value.Length > 0 ? $"<br><color=orange>Quest '{requirements.quest.Value}' completed</color>" : "";
            hoverTooltipArea.Init(ItemUiContext.Instance.Tooltip, $"<size=150%><b><color=red>{locationName} is Locked!</color></b></size><br>Unlock Requirements:<br><color=green>Level {requirements.level.Value}</color>{questText}", true);
        }

        private static void PlayUnlockAnimation(LocationButton locationButton) { return; } // Add eventually

        [PatchPostfix]
        static void Postfix(LocationButton __instance, LocationSettingsClass.Location location, ref UISpawnableToggle ____spawnableToggle, ref Image ____iconImage, ref GameObject ____lockedIcon, ref GameObject ____bossIcon, ref GameObject ____infoPanel, ref CustomTextMeshProUGUI ____infoText)
        {
            // SPTMapProgression.LogSource.LogDebug($"Location button location name: '{location.Name}'");
            if (!MapProgressionManager.IsLocationUnlocked(location.Name))
            {
                LockMap(__instance, location, ____spawnableToggle, ____lockedIcon, ____bossIcon, ____iconImage, ____infoPanel, ____infoText);
            }
        }

    }
}
