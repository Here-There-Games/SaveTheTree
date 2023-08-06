using System;
using UnityEngine;

[Serializable]
public class EntityAttribute
{
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
    }
    
    public void Add(float amount)
    {
        if(amount < 0)
            throw new ArgumentOutOfRangeException();
        amount = Mathf.Clamp(amount + Value,0, MaxValue);
        Value += amount;
    }
}