namespace Core.StatSystem.Interfaces
{
    public interface IAttribute : IStat
    {
        float MaxValue { get; }
    }
}