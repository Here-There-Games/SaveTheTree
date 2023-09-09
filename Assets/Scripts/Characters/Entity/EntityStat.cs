using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Characters.Entity
{
    [Serializable]
    public class EntityStat : INameable
    {
        public event UnityAction<float> OnChangeEvent;
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public EntityStat(float value) : this("", value) { }

        public EntityStat(string name, float value)
        {
            Name = name;
            Value = value;
        }

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

        protected virtual void CheckValue()
        {
            Value = Mathf.Clamp(Value, 0, Value);
            OnChangeEvent?.Invoke(Value);
        }
    }
}