using System.IO;
using EFT;
using Newtonsoft.Json;
using SPT.Reflection.Utils;

namespace SPTMapProgression.ModData;

internal static class ModSaveDataManager
{
    private static string SaveFolderPath => Path.Combine(BepInEx.Paths.PluginPath, "SPTMapProgression", "Data");
    private static string _saveFilePath;
    private static string OldFolderPath => Path.Combine(BepInEx.Paths.PluginPath, "SPTMapProgression");
    private static bool _initialized;
    private static bool _migratedData;
    private static string _playerId;

    public static ModSaveData Data { get; private set; } = new();

    public static void Init(Profile profile)
    {
        if (_initialized) return;
        _playerId = profile.Id;
        if (_playerId == null || _playerId.Length <= 0)
        {
            SptMapProgression.LogSource.LogError("Your PlayerId was null.");
            return;
        }
        _initialized = true;
        _saveFilePath = Path.Combine(SaveFolderPath, $"{_playerId}.json");
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
        Directory.CreateDirectory(SaveFolderPath);
        File.WriteAllText(_saveFilePath, JsonConvert.SerializeObject(Data, Formatting.Indented));
        if (_migratedData)
        {
            File.Delete(Path.Combine(OldFolderPath, $"{_playerId}.json"));
            _migratedData = false;
        }
    }

    private static void Load()
    {
        if (File.Exists(Path.Combine(OldFolderPath, $"{_playerId}.json")))
        {
            try
            {
                Data = JsonConvert.DeserializeObject<ModSaveData>(File.ReadAllText(Path.Combine(OldFolderPath, $"{_playerId}.json"))) ?? new ModSaveData();
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

    // private static string MigrateData()
    // {
    //     
    // }
    
}