using System;

namespace Core.StatSystem.Interfaces
{
    public interface IStat
    {
        event Action<IStat> ChangeValueEvent; 
        string ID { get; }
        float Value { get; }
        void ChangeValue(float amount);
        IStat Clone();
    }
}