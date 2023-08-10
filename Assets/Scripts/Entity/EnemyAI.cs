using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    public abstract class EnemyAI : MonoBehaviour, IFloat, IDead
    {
        public event UnityAction DiedEvent;
        public float Value => experience;
        [field: SerializeField] protected float experience { get; private set; }

        protected Tree tree { get; private set; }
        private IControllable controllable;

        private void Awake()
        {
            controllable = GetComponent<IControllable>();
            tree = FindObjectOfType<Tree>();

            InitializeAwake();
        }

        private void FixedUpdate()
        {
            Vector2 direction = GetDirection();
            controllable.Move(direction);
        }

        public void Dead()
        {
            tree.GetComponent<StatHandle>().Level.AddExperience(this);
            DiedEvent?.Invoke();
            Destroy(gameObject);
        }

        protected abstract void InitializeAwake();

        protected abstract Vector2 GetDirection();
    }
}