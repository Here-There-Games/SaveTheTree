using Common;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    [System.Serializable]
    public class EntityAttack : IDamage
    {
        public event UnityAction OnAttack;
        public event UnityAction OnReadyAttack;

        public bool CanAttack => timer.Stopped;
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public LayerMask Layer { get; private set; }

        [SerializeField] private float cooldown;

        protected Timer timer;

        public void Init(MonoBehaviour mono)
        {
            timer = new Timer(mono, cooldown);
            timer.OnStart += () => OnAttack?.Invoke();
            timer.OnEnd += () => OnReadyAttack?.Invoke();
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
            => collider.GetComponent<ITakeDamage>().TakeDamage(this);
    }
}