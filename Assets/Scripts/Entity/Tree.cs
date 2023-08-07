using Interfaces;
using Mechanics;
using UnityEngine;

namespace Entity
{
    public class Tree : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private EntityAttribute health;

        public void TakeDamage(IDamage damageI)
        {
            health.Spend(damageI.Damage);
        }
    }

    [System.Serializable]
    public class TreeStage
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public float AddHPPercent { get; private set; }
    }
}