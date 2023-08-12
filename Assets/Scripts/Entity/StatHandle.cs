using Interfaces;
using Mechanics;
using UnityEngine;

namespace Entity
{
    public class StatHandle : MonoBehaviour, ITakeDamage
    {
        [field: SerializeField] public EntityAttribute Health { get; private set; }
        [field: SerializeField] public EntityLevel Level { get; private set; }

        private IDead iDead;

        private void Awake()
        {
            iDead = GetComponent<IDead>();
            Health.ChangeValueEvent += hp =>
                                        {
                                            if(hp <= 0)
                                                Dead();
                                        };
        }

        public void TakeDamage(IDamage damageI)
        {
            Health.Spend(damageI.Damage);
        }

        private void Dead()
            => iDead.Dead();
    }
}