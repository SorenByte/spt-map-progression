using System.IO;
using Newtonsoft.Json;
using SPT.Reflection.Utils;

namespace SPTMapProgression.ModData;

internal static class ModSaveDataManager
{
    private static string _saveFilePath;
        // Path.Combine(BepInEx.Paths.PluginPath, "SPTMapProgression", (ClientAppUtils.GetMainApp().GetClientBackEndSession().Profile.AccountId + ".json"));
    internal static bool Initialized = false;

    public static ModSaveData Data { get; private set; } = new();

    public static void Init()
    {
        Initialized = true;
        string playerId = ClientAppUtils.GetMainApp()?.GetClientBackEndSession()?.Profile?.Id;
        // string playerId = ClientAppUtils.GetMainApp()?.GetClientBackEndSession()?.Profile?.AccountId;
        // string playerId = ClientAppUtils.GetMainApp()?.GetClientBackEndSession()?.Profile?.ProfileId;
        if (playerId == null || playerId.Length <= 0)
        {
            SptMapProgression.LogSource.LogError("PlayerId was null.");
        }
        SptMapProgression.LogSource.LogDebug($"PlayerId is: {playerId}");
        _saveFilePath = Path.Combine(BepInEx.Paths.PluginPath, "SPTMapProgression", $"{playerId}.json");
        Load();
        Data.ProfileName = ClientAppUtils.GetMainApp()?.GetClientBackEndSession()?.Profile?.Nickname;
    }

    public static void Shutdown()
    {
        Save();
    }
    
    private static void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_saveFilePath));
        File.WriteAllText(_saveFilePath, JsonConvert.SerializeObject(Data, Formatting.Indented));
    }

    private static void Load()
    {
        if (!File.Exists(_saveFilePath))
        {
            Data = new ModSaveData();
            return;
        }

        try
        {
            Data = JsonConvert.DeserializeObject<ModSaveData>(File.ReadAllText(_saveFilePath))
                   ?? new ModSaveData();
        }
        catch
        {
            Data = new ModSaveData();
        }
    }
}