public enum StatModType
{
    Flat = 10,
    Percent = 20
};

public class StatModifier
{
    public readonly float value;
    public readonly StatModType type;
    public readonly object source;
    public readonly int order;
    public readonly string identifier;
    public StatModifier(float value, StatModType statType, object source)
    {
        this.value = value;
        this.source = source;
        this.order = (int)statType;
        this.type = statType;
    }
    public StatModifier(float value, StatModType statType, object source,string identifier)
    {
        this.value = value;
        this.source = source;
        this.order = (int)statType;
        this.type = statType;
        this.identifier = identifier;
    }
}