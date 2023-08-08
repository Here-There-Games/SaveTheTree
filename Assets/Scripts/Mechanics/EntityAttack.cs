using System;
using Common;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    [Serializable]
    public class EntityAttack: IDamage
    {
        public event UnityAction OnAttack;

        public bool Can => timer.Stopped;
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public LayerMask Layer { get; private set; }
        
        [SerializeField] private float cooldown;

        private Timer timer;

        public void Init(MonoBehaviour mono)
        {
            timer = new Timer(mono, cooldown);
        }

        public bool TryAttack(Collider2D collider)
        {
            if(timer.Stopped){
                Attack(collider);
                timer.Start();
                return true;
            }

            return false;
        }

        private void Attack(Collider2D collider)
        {
            collider.GetComponent<ITakeDamage>().TakeDamage(this);
            OnAttack?.Invoke();
        }
    }
}