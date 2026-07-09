namespace SPTMapProgression.MapProgression;

public class MapProgressionRequirements(string quest, int level, bool transit)
{
    public readonly string Quest = quest;
    public readonly int Level = level;
    public readonly bool Transit = transit;
}