namespace Core.StatSystem.Interfaces
{
    public interface IBuff
    {
        string ID { get; }
        Stat Stat { get; }
        Stats ApplyStats(Stats stats);
    }
}