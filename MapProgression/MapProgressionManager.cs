using System.Collections.Generic;
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
        return _requirements[locationName];
    }    public MapProgressionRequirements GetRequirementsOrDefault(string locationName, MapProgressionRequirements mapProgressionRequirements)
    {
        
        return _requirements.GetValueOrDefault(locationName, mapProgressionRequirements);
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