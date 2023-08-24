using System;
using Characters.Entity;
using Data;
using Interfaces;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    public class StatHandle : MonoBehaviour, ITakeDamage
    {
        private const string HEALTH_ATTRIBUTE_NAME = "Health";

        public event UnityAction<float> OnApplyDamageEvent;

        [field: SerializeField] public StatData StatData { get; private set; }
        [SerializeField] private float maxHealth;
        [field: SerializeField] public EntityLevel Level { get; private set; }

        private IDead iDead;

        private void Awake()
        {
            if(StatData.TryCreateAttribute(new EntityAttribute(HEALTH_ATTRIBUTE_NAME, maxHealth, maxHealth))){
                Debug.Log($"Health was created in {name}");    
            }
            
            EntityAttribute health = StatData.FindAttribute(HEALTH_ATTRIBUTE_NAME);
            OnApplyDamageEvent += value => { health.Spend(value); };
            health.OnChangeEvent += hp =>
                                        {
                                            if(hp <= 0)
                                                Dead();
                                        };

            iDead = GetComponent<IDead>();
        }

        public EntityStat GetStat(string statName)
        {
            if(StatData.TryGetStat(statName, out EntityStat stat))
                return stat;
            else
                throw new NullReferenceException($"{name} is not found a {statName} stat");
        }
        
        public EntityAttribute GetAttribute(string attributeName)
        {
            if(StatData.TryGetAttribute(attributeName, out EntityAttribute attribute))
                return attribute;
            else
                throw new NullReferenceException($"{name} is not found a {attributeName} stat");
        }
        
        public void TakeDamage(IDamage damageI)
        {
            OnApplyDamageEvent?.Invoke(damageI.Damage);
            // Health.Spend(damageI.Damage);
        }

        private void Dead()
            => iDead.Dead();
    }
}