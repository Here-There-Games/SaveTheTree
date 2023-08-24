using System;
using Characters.Entity;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    [Serializable]
    public class EntityAttribute : EntityStat
    {
        public event UnityAction<float> ChangeValueNormalizedEvent;

        [field: SerializeField] public float MaxValue { get; private set; }

        public EntityAttribute(float maxValue) : this("", maxValue, maxValue) { }

        public EntityAttribute(string name, float value, float maxValue) : base(name, value)
        {
            MaxValue = maxValue;
        }

        public float GetNormalized()
            => Value / MaxValue;

        public void UpgradeAttribute(float value)
        {
            MaxValue += value;
            MaxValue = Mathf.Clamp(MaxValue, 0, MaxValue);
        }

        protected override void CheckValue()
        {
            base.CheckValue();
            ChangeValueNormalizedEvent?.Invoke(Value / MaxValue);
        }

        public void RestoreAttribute()
            => Add(MaxValue);

        public void AddToMaxValue(float amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException();

            MaxValue += amount;
        }
    }
}