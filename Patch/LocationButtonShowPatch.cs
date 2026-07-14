using System;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Collections;
using System.Reflection;
using Comfort.Common;
using EFT;
using SPT.Reflection.Utils;
using SPTMapProgression.Config;
using SPTMapProgression.MapProgression;
using SPTMapProgression.ModData;
using SPTMapProgression.TarkovMap;
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

        private static bool _unlockSoundPlayed;
        
        private static void LockMap(LocationButton locationButton, LocationSettingsClass.Location location, bool isScav, UISpawnableToggle spawnableToggle, GameObject lockedIcon, GameObject bossIcon, Image iconImage, GameObject infoPanel)
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
            
            MapProgressionManager progressionManager;
            if (isScav)
            { 
                progressionManager = SptMapProgression.ScavMapProgressionManager;
            }
            else
            { 
                progressionManager = SptMapProgression.PmcMapProgressionManager;
            }

            string locationDisplayName = TarkovMapClass.ToDisplayName(location.Name);
            ClientConfigDefault clientConfig = SptMapProgression.ClientConfig;
            MapProgressionRequirements requirements = progressionManager.GetRequirements(location.Name);
            Profile profile = ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile;

            int levelValue = requirements.LevelConfigEntry.Value;
            string questIdValue = requirements.QuestIdConfigEntry.Value;
            string questNameValue = requirements.QuestDisplayNameEntry.Value;
            bool transitValue = requirements.TransitConfigEntry.Value;
            int surviveValue = requirements.SurviveConfigEntry.Value;
            int equipmentValueValue = requirements.EquipmentValueConfigEntry.Value;

            string incomplete = $"<mark=#52525280 padding=\"30em,30em,0em,0em\"><color=#868686>{clientConfig.IncompleteSymbol.Value} <color=#FFFFFF>";
            string finished = $"<mark=#00283d99 padding=\"30em,30em,0em,0em\"><color=#1ADFFF>{clientConfig.FinishedSymbol.Value} <color=#B8F5FF>";
            
            string levelTextPrefix = MapProgressionHelper.IsLevelSufficient(levelValue) ? finished : incomplete;
            string levelText = levelValue > 0 ? $"<br>{levelTextPrefix}{string.Format(clientConfig.LevelText.Value, levelValue)}</color>" : "";
            string questTextPrefix = MapProgressionHelper.IsQuestCompleted(questIdValue) ? finished : incomplete;
            string questText = questIdValue.Length > 0 ? $"<br>{questTextPrefix}{string.Format(clientConfig.QuestText.Value, questNameValue)}</color>" : "";
            string transitTextPrefix = MapProgressionHelper.HasTransited(location.Name) ? finished : incomplete;
            string transitText = transitValue ? $"<br>{transitTextPrefix}{clientConfig.TransitText.Value}</color>" : "";
            string surviveTextPrefix = MapProgressionHelper.IsSurvivesSufficient(location.Name, surviveValue) ? finished : incomplete;
            int currentSurvives = MapProgressionHelper.GetMapSurvives(location.Name);
            string surviveText = surviveValue > 0 ? $"<br>{surviveTextPrefix}{string.Format(clientConfig.SurviveText.Value, currentSurvives, surviveValue)}</color>" : "";
            string equipmentValueTextPrefix = MapProgressionHelper.IsEquipmentValueSufficient(equipmentValueValue) ? finished : incomplete;
            int currentEquipmentValue = MapProgressionHelper.GetEquipmentValue(profile);
            string currentEquipmentValueText = equipmentValueValue > 0 ? $"<br>{equipmentValueTextPrefix}{string.Format(clientConfig.EquipmentValueText.Value, MathUtility.GetFormattedNumber(Math.Clamp(currentEquipmentValue, 0, equipmentValueValue)), MathUtility.GetFormattedNumber(equipmentValueValue))}</color>" : "";
            hoverTooltipArea.Init(ItemUiContext.Instance.Tooltip, $"<align=center><size=150%><b><color=red>{string.Format(clientConfig.LockedText.Value, locationDisplayName)}</align></color></b></size><br><align=center><size=130%>{clientConfig.RequirementsText.Value}</align></size><br><line-height=150%><align=center>{clientConfig.RequirementsSymbol.Value}</align><align=center><b>{levelText}</mark>{questText}</mark>{transitText}</mark>{surviveText}</mark>{currentEquipmentValueText}</mark>", true);
        }

        private static void PlayUnlockAnimation(LocationButton locationButton, GameObject bossIcon, Image iconImage, GameObject newIcon)
        {
            if (SptMapProgression.ClientConfig.ShouldPlaySound.Value && !_unlockSoundPlayed) Singleton<GUISounds>.Instance.PlayUISound(EUISoundType.AchievementCompleted);
            _unlockSoundPlayed = true;
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
            _unlockSoundPlayed = false;

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
            bool isScav = LocationScreenShowPatch.CurrentRaidSettings.IsScav;
            bool isMapUnlocked = MapProgressionHelper.IsLocationUnlockedForSide(location.Name, isScav);
            if (!isMapUnlocked)
            {
                LockMap(__instance, location, isScav, ____spawnableToggle, ____lockedIcon, ____bossIcon, ____iconImage, ____infoPanel);
                return;
            }

            if (isScav)
            {
                if (ModSaveDataManager.Data.UnlockAnimationsPlayedScav.Add(location.Name))
                {
                    ModSaveDataManager.Save();
                    PlayUnlockAnimation(__instance, ____bossIcon, ____iconImage, ____newIcon);
                }
            } else
            {
                if (ModSaveDataManager.Data.UnlockAnimationsPlayedPmc.Add(location.Name))
                {
                    ModSaveDataManager.Save();
                    PlayUnlockAnimation(__instance, ____bossIcon, ____iconImage, ____newIcon);
                }
            }
        }

    }
}
