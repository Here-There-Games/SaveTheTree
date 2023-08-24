using Interfaces;
using UnityEngine;

namespace Entity
{
    public class Item : MonoBehaviour, IFloat
    {
        [field: SerializeField] public float Value { get; private set; }

        public void SetExperience(float value)
        {
            Value = value;
        }

        public void Pickup()
        {
            Destroy(gameObject);
        }
    }
}