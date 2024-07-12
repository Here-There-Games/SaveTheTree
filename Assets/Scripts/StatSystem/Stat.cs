using Core.StatSystem.Interfaces;
using System;
using UnityEngine;

namespace Core.StatSystem
{
    [Serializable]
    public struct Stat : IStat
    {
        public event Action<IStat> ChangeValueEvent;
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        
        public Stat(string name, float value = 1f) : this()
        {
            ID = name;
            Value = value;
        }

        public void ChangeValue(float amount)
        {
            Value = Mathf.Max(0, amount + Value);
            ChangeValueEvent?.Invoke(this);
        }

        public static bool operator ==(Stat firstStat, Stat secondStat)
            => CompareName(firstStat, secondStat) &&
               CompareValue(firstStat, secondStat);

        public static bool operator !=(Stat firstStat, Stat secondStat) => !(firstStat == secondStat);
        public bool Equals(Stat other) => this == other;

        public static bool CompareName(IStat firstStat, IStat secondStat)
            => !string.IsNullOrEmpty(firstStat.ID) && !string.IsNullOrEmpty(secondStat.ID) &&
               string.Equals(firstStat.ID, secondStat.ID, StringComparison.CurrentCultureIgnoreCase);

        public static bool CompareValue(Stat firstStat, Stat secondStat)
            => Math.Abs(firstStat.Value - secondStat.Value) < 0.1f;

        public override bool Equals(object obj) => obj is Stat other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(ID, Value);

        public override string ToString() => string.IsNullOrEmpty(ID) ? string.Empty : $"{ID}:{Value}";

        public IStat Clone()
            => new Stat(ID, Value);
    }
}