using System;

namespace SPTMapProgression.Utility;

public static class StringUtility
{
    
    public static string GetMapDisplayName(string mapId)
    {
        switch (mapId.ToLower())
        {
            case "sandbox" or "sandbox_high":
                return SptMapProgression.ClientConfig.GroundZeroText.Value;
            case "factory" or "factory4_day" or "factory4_night":
                return SptMapProgression.ClientConfig.FactoryText.Value;
            case "customs" or "bigmap":
                return SptMapProgression.ClientConfig.CustomsText.Value;
            case "woods":
                return SptMapProgression.ClientConfig.WoodsText.Value;
            case "reservebase" or "rezervbase":
                return SptMapProgression.ClientConfig.ReserveText.Value;
            case "shoreline":
                return SptMapProgression.ClientConfig.ShorelineText.Value;
            case "lighthouse":
                return SptMapProgression.ClientConfig.LighthouseText.Value;
            case "interchange":
                return SptMapProgression.ClientConfig.InterchangeText.Value;
            case "streets of tarkov" or "tarkovstreets":
                return SptMapProgression.ClientConfig.StreetsOfTarkovText.Value;
            case "laboratory":
                return SptMapProgression.ClientConfig.TheLabText.Value;
            case "labyrinth":
                return SptMapProgression.ClientConfig.TheLabyrinthText.Value;
            case "terminal":
                return SptMapProgression.ClientConfig.TerminalText.Value;
            default:
                return mapId;
        }
    }
    
    public static string GetMapName(string mapId)
    {
        var config = SptMapProgression.ClientConfig;
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
    
    public static string GetMapInternalName(string mapId)
    {
        var config = SptMapProgression.ClientConfig;
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