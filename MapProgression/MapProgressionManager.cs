using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace SPTMapProgression.MapProgression;

public class MapProgressionManager
{
    private Dictionary<string, MapProgressionRequirements> _requirements = [];

    public MapProgressionManager AddRequirements(string locationName, MapProgressionRequirements mapProgressionRequirements)
    { 
        _requirements.Add(locationName, mapProgressionRequirements);
        return this;
    }

    [CanBeNull]
    public MapProgressionRequirements GetRequirements(string locationName)
    {
        return _requirements.GetValueOrDefault(locationName, null);
    }
    public MapProgressionRequirements GetRequirementsOrDefault(string locationName, MapProgressionRequirements mapProgressionRequirements)
    {
        
        return _requirements.GetValueOrDefault(locationName, mapProgressionRequirements);
    }
    public MapProgressionRequirements GetRequirementsOrDefault(string locationName)
    {
        
        return _requirements.GetValueOrDefault(locationName, _requirements[_requirements.Keys.ElementAt(0)]);
    }
    public Dictionary<string, MapProgressionRequirements>.KeyCollection GetKeys()
    {
        return _requirements.Keys;
    }
    
    public bool ContainsLocation(string locationName)
    {
        return _requirements.ContainsKey(locationName);
    }

    public bool ContainsLocation(LocationSettingsClass.Location location)
    {
        return _requirements.ContainsKey(location.Name);
    }

}