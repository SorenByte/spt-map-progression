using System.Collections.Generic;

namespace SPTMapProgression.ModData;

public class ModSaveData
{
    public string ProfileName;
    public HashSet<string> MapTransits { get; set; }  = [];
    public HashSet<string> UnlockAnimationsPlayedPmc { get; set; } = [];
    public HashSet<string> UnlockAnimationsPlayedScav { get; set; } = [];
    public Dictionary<string, int> MapSurvives { get; set; } = [];
}