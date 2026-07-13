using System;
using System.Linq;
using BepInEx.Configuration;

namespace SPTMapProgression.TarkovMap;

public class TarkovMapClass
{

    public static readonly TarkovMapClass GroundZero = new TarkovMapClass("Sandbox", SptMapProgression.ClientConfig.GroundZeroText, "Sandbox", "Sandbox_high");
    public static readonly TarkovMapClass Factory = new TarkovMapClass("Factory", SptMapProgression.ClientConfig.FactoryText, "factory4_day", "factory4_night", "factory");
    public static readonly TarkovMapClass Customs = new TarkovMapClass("Customs", SptMapProgression.ClientConfig.CustomsText, "bigmap");
    public static readonly TarkovMapClass Woods = new TarkovMapClass("Woods", SptMapProgression.ClientConfig.WoodsText);
    public static readonly TarkovMapClass Reserve = new TarkovMapClass("ReserveBase", SptMapProgression.ClientConfig.ReserveText, "RezervBase");
    public static readonly TarkovMapClass Shoreline = new TarkovMapClass("Shoreline", SptMapProgression.ClientConfig.ShorelineText);
    public static readonly TarkovMapClass Lighthouse = new TarkovMapClass("Lighthouse", SptMapProgression.ClientConfig.LighthouseText);
    public static readonly TarkovMapClass Interchange = new TarkovMapClass("Interchange", SptMapProgression.ClientConfig.InterchangeText);
    public static readonly TarkovMapClass StreetsOfTarkov = new TarkovMapClass("Streets of Tarkov", SptMapProgression.ClientConfig.StreetsOfTarkovText, "TarkovStreets");
    public static readonly TarkovMapClass TheLab = new TarkovMapClass("Laboratory", SptMapProgression.ClientConfig.TheLabText, "laboratory");
    public static readonly TarkovMapClass TheLabyrinth = new TarkovMapClass("Labyrinth", SptMapProgression.ClientConfig.TheLabyrinthText);
    public static readonly TarkovMapClass Terminal = new TarkovMapClass("Terminal", SptMapProgression.ClientConfig.TerminalText);

    public static readonly TarkovMapClass[] AllMaps =
    [
        GroundZero, Factory, Customs, Woods, Reserve, Shoreline,
        Lighthouse, Interchange, StreetsOfTarkov, TheLab, TheLabyrinth, Terminal
    ];

    public string Name { get; }
    public string InternalName { get; }
    public ConfigEntry<string> DisplayName { get; }

    private TarkovMapClass(string name, ConfigEntry<string> displayName, string internalName = null, params string[] aliases)
    {
        Name = name;
        InternalName = internalName ?? name;
        DisplayName = displayName;
    }
    
    public static TarkovMapClass FromString(string mapName)
    {
        return AllMaps.FirstOrDefault(map => map.Matches(mapName));
    }

    public static string ToDisplayName(string mapName)
    {
        var map = FromString(mapName);
        return map == null ? mapName : map.DisplayName.Value;
    }

    public static string ToName(string mapName)
    {
        var map = FromString(mapName);
        return map == null ? mapName : map.Name;
    }

    public static string ToInternalName(string mapName)
    {
        var map = FromString(mapName);
        return map == null ? mapName : map.InternalName;
    }

    public bool Matches(string mapName)
    {
        if (string.IsNullOrEmpty(mapName)) return false;
        if (mapName.Equals(InternalName, StringComparison.OrdinalIgnoreCase) || mapName.Equals(Name, StringComparison.OrdinalIgnoreCase) || mapName.Equals(DisplayName.Value, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }
}