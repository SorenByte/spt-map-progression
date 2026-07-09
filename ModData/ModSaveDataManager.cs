using System.IO;
using Newtonsoft.Json;

namespace SPTMapProgression.ModData;

public static class ModSaveDataManager
{
    private static string SaveFilePath =>
        Path.Combine(BepInEx.Paths.PluginPath, "SPTMapProgression", "save.json");

    public static ModSaveData Data { get; private set; } = new();

    public static void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SaveFilePath));
        File.WriteAllText(SaveFilePath, JsonConvert.SerializeObject(Data, Formatting.Indented));
    }

    public static void Load()
    {
        if (!File.Exists(SaveFilePath))
        {
            Data = new ModSaveData();
            return;
        }

        try
        {
            Data = JsonConvert.DeserializeObject<ModSaveData>(File.ReadAllText(SaveFilePath))
                   ?? new ModSaveData();
        }
        catch
        {
            Data = new ModSaveData();
        }
    }
}