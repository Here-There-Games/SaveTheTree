using Interfaces;
using Mechanics;
using UnityEngine;

namespace Entity
{
    public class StatHandle : MonoBehaviour, ITakeDamage
    {
        [field:SerializeField] public EntityAttribute Health{ get; private set; }
        [field:SerializeField] public EntityLevel Level { get; private set; }

        public void TakeDamage(IDamage damageI)
        {
            Health.Spend(damageI.Damage);
        }
    }
}