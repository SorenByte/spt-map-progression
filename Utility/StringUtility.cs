using System;

namespace SPTMapProgression.Utility;

public static class StringUtility
{
    
    // MapProgressionManager.AddRequirements("Sandbox", new MapProgressionRequirements("", 0, false))
    //     .AddRequirements("Factory", new MapProgressionRequirements("Shooting Cans", 2, false))
    //     .AddRequirements("Customs", new MapProgressionRequirements("Debut", 4, false))
    //     .AddRequirements("Woods", new MapProgressionRequirements("Luxurious Life", 6, false))
    //     .AddRequirements("ReserveBase", new MapProgressionRequirements("Belka and Strelka", 8, false))
    //     .AddRequirements("Shoreline", new MapProgressionRequirements("The Bunker - Part 1", 10, false))
    //     .AddRequirements("Lighthouse", new MapProgressionRequirements("Chemical - Part 3", 12, false))
    //     .AddRequirements("Interchange", new MapProgressionRequirements("Only Business", 14, true))
    //     .AddRequirements("Streets of Tarkov", new MapProgressionRequirements("Population Census", 16, true))
    //     .AddRequirements("Laboratory", new MapProgressionRequirements("Beneath The Streets", 18, true))
    //     .AddRequirements("Labyrinth", new MapProgressionRequirements("Indisputable Authority", 20, true))
    //     .AddRequirements("Terminal", new MapProgressionRequirements("", 100, false)); // Impossible to unlock
    
    public static string GetMapDisplayName(string mapId)
    {
        switch (mapId.ToLower())
        {
            case "sandbox" or "sandbox_high":
                return SptMapProgression.BepinConfig.GroundZeroText.Value;
            case "factory" or "factory4_day" or "factory4_night":
                return SptMapProgression.BepinConfig.FactoryText.Value;
            case "customs" or "bigmap":
                return SptMapProgression.BepinConfig.CustomsText.Value;
            case "woods":
                return SptMapProgression.BepinConfig.WoodsText.Value;
            case "reservebase" or "rezervbase":
                return SptMapProgression.BepinConfig.ReserveText.Value;
            case "shoreline":
                return SptMapProgression.BepinConfig.ShorelineText.Value;
            case "lighthouse":
                return SptMapProgression.BepinConfig.LighthouseText.Value;
            case "interchange":
                return SptMapProgression.BepinConfig.InterchangeText.Value;
            case "streets of tarkov" or "tarkovstreets":
                return SptMapProgression.BepinConfig.StreetsOfTarkovText.Value;
            case "laboratory":
                return SptMapProgression.BepinConfig.TheLabText.Value;
            case "labyrinth":
                return SptMapProgression.BepinConfig.TheLabyrinthText.Value;
            case "terminal":
                return SptMapProgression.BepinConfig.TerminalText.Value;
            default:
                return mapId;
        }
    }
    
    public static string GetMapInternalName(string mapId)
    {
        var config = SptMapProgression.BepinConfig;
        string id = mapId.ToLower();

        if (id is "sandbox_high" || Matches(mapId, config.GroundZeroText.Value))
            return "Sandbox";
        if (id is "factory4_day" or "factory4_night" || Matches(mapId, config.FactoryText.Value))
            return "Factory";
        if (id is "bigmap" || Matches(mapId, config.CustomsText.Value))
            return "Customs";
        if (Matches(mapId, config.WoodsText.Value))
            return "Woods";
        if (id is "rezervbase" || Matches(mapId, config.ReserveText.Value))
            return "ReserveBase";
        if (Matches(mapId, config.ShorelineText.Value))
            return "Shoreline";
        if (Matches(mapId, config.LighthouseText.Value))
            return "Lighthouse";
        if (Matches(mapId, config.InterchangeText.Value))
            return "Interchange";
        if (id is "tarkovstreets" || Matches(mapId, config.StreetsOfTarkovText.Value))
            return "Streets of Tarkov";
        if (id is "laboratory" || Matches(mapId, config.TheLabText.Value))
            return "Laboratory";
        if (Matches(mapId, config.TheLabyrinthText.Value))
            return "Labyrinth";
        if (Matches(mapId, config.TerminalText.Value))
            return "Terminal";

        return mapId;
    }

    private static bool Matches(string mapId, string displayName)
    {
        return mapId.Equals(displayName, StringComparison.OrdinalIgnoreCase);
    }
    
}