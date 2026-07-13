using System.IO;
using EFT;
using Newtonsoft.Json;
using SPT.Reflection.Utils;

namespace SPTMapProgression.ModData;

internal static class ModSaveDataManager
{
    private static string SaveFolderPath => Path.Combine(BepInEx.Paths.PluginPath, "SPTMapProgression");
    private static string _saveFilePath;
    private static readonly string OldSavePath = Path.Combine(SaveFolderPath, "save.json");
    private static bool _initialized;
    private static bool _migratedData;

    public static ModSaveData Data { get; private set; } = new();

    public static void Init(Profile profile)
    {
        if (_initialized) return;
        string playerId = profile.Id;
        if (playerId == null || playerId.Length <= 0)
        {
            SptMapProgression.LogSource.LogError("Your PlayerId was null.");
            return;
        }
        _initialized = true;
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
        if (!_initialized) return;
        Directory.CreateDirectory(Path.GetDirectoryName(_saveFilePath));
        File.WriteAllText(_saveFilePath, JsonConvert.SerializeObject(Data, Formatting.Indented));
        if (_migratedData) File.Delete(OldSavePath); // Get rid of the old save.json once the data has been saved
    }

    private static void Load()
    {
        if (File.Exists(OldSavePath))
        {
            try
            {
                Data = JsonConvert.DeserializeObject<ModSaveData>(File.ReadAllText(OldSavePath)) ?? new ModSaveData();
                _migratedData = true;
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