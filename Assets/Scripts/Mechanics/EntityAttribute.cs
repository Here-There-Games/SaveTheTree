using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

namespace Mechanics
{
    [Serializable]
    public class EntityAttribute
    {
        public event UnityAction<float> OnChangeValue;

        [field: SerializeField]public float Value{get; private set;}
        [field: SerializeField]public float MaxValue{get; private set;}

        public float GetNormalized()
            => Value / MaxValue;

        public void Spend(float amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException();
            Value -= amount;
            CheckValue();
        }

        private void CheckValue()
        {
            Value = Mathf.Clamp(Value,0, MaxValue);
            OnChangeValue?.Invoke(Value);
        }
    
        public void Add(float amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException();
            Value += amount;
            CheckValue();
        }
    }
}