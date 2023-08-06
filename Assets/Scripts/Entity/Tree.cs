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
}