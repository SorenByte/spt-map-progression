using System.Collections.Generic;

namespace SPTMapProgression.ModData;

public class ModSaveData
{
    public string ProfileName;
    public HashSet<string> MapTransits { get; set; }  = [];
    public HashSet<string> UnlockAnimationsPlayed { get; set; } = [];
    
}