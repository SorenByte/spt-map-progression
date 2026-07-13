using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace SPTMapProgression.MapProgression;

public class MapProgressionManager(ConfigFile config, string side)
{
    private readonly List<MapProgressionRequirements> _requirements = [];

    internal ConfigFile Config = config;
    internal string Side = side;
    
    public MapProgressionManager AddRequirements(MapProgressionRequirements mapProgressionRequirements)
    {
        _requirements.Add(mapProgressionRequirements);
        return this;
    }
    public MapProgressionRequirements GetRequirements(string locationName)
    {
        return _requirements.FirstOrDefault(requirements => requirements.Map == locationName);
    }
    public bool ContainsLocation(string locationName)
    {
        return _requirements.Any(requirements => requirements.Map == locationName);
    }
    public bool ContainsLocation(LocationSettingsClass.Location location)
    {
        return ContainsLocation(location.Name);
    }

}