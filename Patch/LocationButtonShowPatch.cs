using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BepInEx.Configuration;
using Comfort.Common;
using SPTMapProgression.Config;
using SPTMapProgression.Helper;
using SPTMapProgression.MapProgression;
using SPTMapProgression.ModData;
using SPTMapProgression.Utility;
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

        private static void LockMap(LocationButton locationButton, LocationSettingsClass.Location location, UISpawnableToggle spawnableToggle, GameObject lockedIcon, GameObject bossIcon, Image iconImage, GameObject infoPanel)
        {
            spawnableToggle.HideGameObject();
            lockedIcon.SetActive(true);
            var lockedIconGraphic = lockedIcon.GetComponent<Image>();
            if (lockedIconGraphic != null) lockedIconGraphic.raycastTarget = true;
            bossIcon.SetActive(false);
            iconImage.enabled = false;
            infoPanel.SetActive(false);
            var shadow = locationButton.transform.Find("Shadow");
            if (shadow != null) shadow.gameObject.SetActive(false);
            var background = locationButton.transform.Find("Background");
            if (background != null) background.gameObject.SetActive(false);
            HoverTooltipArea hoverTooltipArea = lockedIcon.GetOrAddComponent<HoverTooltipArea>();
            // string locationName = location.Name;
            string locationName = StringUtility.GetMapDisplayName(location.Name);
            BepinConfigDefault config = SptMapProgression.BepinConfig;
            string incomplete = $"<mark=#52525280 padding=\"30em,30em,0em,0em\"><color=#868686>{config.IncompleteSymbol.Value} <color=#FFFFFF>";
            string finished = $"<mark=#00283d99 padding=\"30em,30em,0em,0em\"><color=#1ADFFF>{config.FinishedSymbol.Value} <color=#B8F5FF>";
            (ConfigEntry<string> quest, ConfigEntry<int> level, ConfigEntry<bool> transit) requirements =
                config.MapRequirements.GetValueOrDefault(location.Name,
                    config.MapRequirements[config.MapRequirements.Keys.ElementAt(0)]);
            string levelTextPrefix = MapProgressionHelper.IsLevelSufficient(location.Name) ? finished : incomplete;
            string levelText = requirements.level.Value > 0 ? $"<br>{levelTextPrefix}{string.Format(config.LevelText.Value, requirements.level.Value)}</color>" : "";
            string questTextPrefix = MapProgressionHelper.IsQuestCompleted(requirements.quest.Value) ? finished : incomplete;
            string questText = requirements.quest.Value.Length > 0 ? $"<br>{questTextPrefix}{string.Format(config.QuestText.Value, requirements.quest.Value)}</color>" : "";
            string transitTextPrefix = MapProgressionHelper.HasTransited(location.Name) ? finished : incomplete;
            string transitText = requirements.transit.Value == true ? $"<br>{transitTextPrefix}{config.TransitText.Value}</color>" : "";
            hoverTooltipArea.Init(ItemUiContext.Instance.Tooltip, $"<align=center><size=150%><b><color=red>{string.Format(config.LockedText.Value, locationName)}</align></color></b></size><br><align=center><size=130%>{config.RequirementsText.Value}</align></size><br><line-height=150%><align=center>{config.RequirementsSymbol.Value}</align><align=center><b>{levelText}</mark>{questText}</mark>{transitText}</mark>", true);
        }

        private static void PlayUnlockAnimation(LocationButton locationButton, GameObject bossIcon, Image iconImage, GameObject newIcon)
        {
            if (SptMapProgression.BepinConfig.ShouldPlaySound.Value) Singleton<GUISounds>.Instance.PlayUISound(EUISoundType.AchievementCompleted);
            bossIcon.SetActive(false);
            iconImage.enabled = false;
            newIcon.SetActive(true);
            
            Image newIconImage = newIcon.GetComponent<Image>();
            locationButton.StartCoroutine(AnimateIcon(newIcon.transform, newIconImage));
        }
        
        private static IEnumerator AnimateIcon(Transform target, Image image)
        {
            const float duration = 1.0f;

            Vector3 startScale = Vector3.one * 5.0f;
            Vector3 endScale   = Vector3.one;
            float startAlpha = 0f;
            float endAlpha   = 1f;

            target.localScale = startScale;

            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                float eased = (float) MathUtility.EaseInOutCirc(t);
                
                target.localScale = Vector3.LerpUnclamped(startScale, endScale, eased);

                if (!image.IsNullOrDestroyed())
                {
                    Color c = image.color;
                    c.a = Mathf.Lerp(startAlpha, endAlpha, eased);
                    image.color = c;
                }

                yield return null;
            }

            target.localScale = endScale;

            if (!image.IsNullOrDestroyed())
            {
                Color c = image.color;
                c.a = endAlpha;
                image.color = c;
            }
        }
        
        [PatchPostfix]
        static void Postfix(LocationButton __instance, LocationSettingsClass.Location location, ref UISpawnableToggle ____spawnableToggle, ref Image ____iconImage, ref GameObject ____lockedIcon, ref GameObject ____bossIcon, ref GameObject ____infoPanel, ref CustomTextMeshProUGUI ____infoText, ref GameObject ____newIcon)
        {
            if (!ModSaveDataManager.Initialized) ModSaveDataManager.Init();
            if (!MapProgressionHelper.IsLocationUnlocked(location.Name))
            {
                // PlayUnlockAnimation(__instance, ____bossIcon, ____iconImage, ____newIcon);
                LockMap(__instance, location, ____spawnableToggle, ____lockedIcon, ____bossIcon, ____iconImage, ____infoPanel);
            }
            else if (ModSaveDataManager.Data.UnlockAnimationsPlayed.Add(location.Name))
            {
                PlayUnlockAnimation(__instance, ____bossIcon, ____iconImage, ____newIcon);
            }
        }

    }
}
