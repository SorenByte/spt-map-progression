using System;
using System.IO;
using EFT;
using Newtonsoft.Json;
using SPT.Reflection.Utils;

namespace SPTMapProgression.ModData;

internal static class ModSaveDataManager
{
    private static string SaveFolderPath => Path.Combine(BepInEx.Paths.PluginPath, "SPTMapProgression");
    private static string _saveFilePath;
    private static string _oldSavePath = Path.Combine(SaveFolderPath, "save.json");
    internal static bool Initialized = false;
    internal static bool MigratedData = false;

    public static ModSaveData Data { get; private set; } = new();

    public static void Init(Profile profile)
    {
        if (Initialized) return;
        string playerId = profile.Id;
        if (playerId == null || playerId.Length <= 0)
        {
            SptMapProgression.LogSource.LogError("Your PlayerId was null.");
            return;
        }
        Initialized = true;
        _saveFilePath = Path.Combine(SaveFolderPath, $"{playerId}.json");
        Load();
        Data.ProfileName = ClientAppUtils.GetMainApp()?.GetClientBackEndSession()?.Profile?.Nickname;
    }

    public static void Shutdown()
    {
        Save();
    }
    
    internal static void Save()
    {
        if (Initialized == false) return;
        Directory.CreateDirectory(Path.GetDirectoryName(_saveFilePath));
        File.WriteAllText(_saveFilePath, JsonConvert.SerializeObject(Data, Formatting.Indented));
        if (MigratedData) File.Delete(_oldSavePath); // Get rid of the old save.json once the data has been saved
    }

    private static void Load()
    {
        if (File.Exists(_oldSavePath))
        {
            try
            {
                Data = JsonConvert.DeserializeObject<ModSaveData>(File.ReadAllText(_oldSavePath)) ?? new ModSaveData();
                MigratedData = true;
                return;
            }
            catch
            {
                Data = new ModSaveData();
                return;
            }
        }
        if (!File.Exists(_saveFilePath))
        {
            Data = new ModSaveData();
            return;
        }

        try
        {
            Data = JsonConvert.DeserializeObject<ModSaveData>(File.ReadAllText(_saveFilePath)) ?? new ModSaveData();
        }
        catch
        {
            Data = new ModSaveData();
        }
    }
}