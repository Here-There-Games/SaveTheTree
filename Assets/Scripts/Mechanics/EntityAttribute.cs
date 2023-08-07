using System;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    [Serializable]
    public class EntityAttribute
    {
        public event UnityAction<float> OnChangeValue;
        public event UnityAction OnZeroValue;

        [field: SerializeField]public float Value{get; private set;}
        [field: SerializeField]public float MaxValue{get; private set;}

        public float GetNormalized()
            => Value / MaxValue;

        public void Spend(float amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException();
            amount = Mathf.Clamp(amount,0, MaxValue);
            Value -= amount;
            OnChangeValue?.Invoke(Value);
            if(Value == 0)
                OnZeroValue?.Invoke();
        }
    
        public void Add(float amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException();
            amount = Mathf.Clamp(amount + Value,0, MaxValue);
            Value += amount;
            OnChangeValue?.Invoke(Value);
        }
    }
}