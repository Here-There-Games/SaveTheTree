using System;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    [Serializable]
    public class EntityAttribute
    {
        public event UnityAction<float> ChangeValueEvent;
        public event UnityAction<float> ChangeValueNormalizedEvent;

        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public float MaxValue { get; private set; }

        public EntityAttribute(float maxValue) : this(maxValue, maxValue) { }

        public EntityAttribute(float value, float maxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }

        public float GetNormalized()
            => Value / MaxValue;

        public void Spend(float amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException();
            Value -= amount;
            CheckValue();
        }

        public void Add(float amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException();
            Value += amount;
            CheckValue();
        }

        public void UpgradeAttribute(float value)
        {
            MaxValue += value;
            MaxValue = Mathf.Clamp(MaxValue, 0, MaxValue);
        }

        private void CheckValue()
        {
            Value = Mathf.Clamp(Value, 0, MaxValue);
            ChangeValueEvent?.Invoke(Value);
            ChangeValueNormalizedEvent?.Invoke(Value / MaxValue);
        }
    }
}