using Core.StatSystem.Interfaces;
using System;
using UnityEngine;

namespace Core.StatSystem
{
    [Serializable]
    public struct Attribute : IAttribute
    {
        public event Action<IStat> ChangeValueEvent;
        public string ID => id;
        public float Value => value;
        public float MaxValue => maxValue > -1 ? maxValue : 9999999;

        [SerializeField] private string id;
        [SerializeField] private float value;
        [SerializeField] private float maxValue;

        public void ChangeValue(float amount)
        {
            value = Mathf.Clamp(value + amount, 0, maxValue);
            ChangeValueEvent?.Invoke(this);
        }

        public void ChangeMax(float newMaxValue)
        {
            maxValue = newMaxValue;
            value = Mathf.Min(Value, MaxValue);
        }

        public IStat Clone() => new Attribute(ID, Value, MaxValue);

        public Attribute(string name, float newValue, float newMaxValue) : this()
        {
            maxValue = newMaxValue;
            id = name;
            value = newValue;
        }

        public override string ToString() => string.IsNullOrEmpty(ID)
                                                 ? string.Empty
                                                 : $"{ID}:" + (Math.Abs(Value - MaxValue) == 0
                                                                   ? $"max({MaxValue})"
                                                                   : (Value) + "\\" + (maxValue > -1 ? MaxValue : "∞"));
    }
}