using Common;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics
{
    [System.Serializable]
    public class EntityAttack : IDamage
    {
        public event UnityAction AttackEvent;
        public event UnityAction OnReadyAttack;

        public bool CanAttack => timer.Stopped;
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public LayerMask Layer { get; private set; }

        [SerializeField] private float cooldown;

        protected Timer timer;

        public void Init(MonoBehaviour mono)
        {
            timer = new Timer(mono, cooldown);
            timer.StartEvent += () => AttackEvent?.Invoke();
            timer.EndEvent += () => OnReadyAttack?.Invoke();
        }

        public bool TryAttack()
        {
            if(timer.Stopped){
                AttackEvent?.Invoke();
                timer.Start();
                return true;
            }

            return false;
        }
    }
}