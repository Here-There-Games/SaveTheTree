using Interfaces;
using Mechanics;
using UnityEngine;

namespace Entity
{
    public abstract class EnemyAI : MonoBehaviour, IFloat, IDead
    {
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

        protected abstract void InitializeAwake();

        protected abstract Vector2 GetDirection();

        private void FixedUpdate()
        {
            Vector2 direction = GetDirection();
            controllable.Move(direction);
        }

        public void Dead()
        {
            tree.GetComponent<StatHandle>().Level.AddExperience(this);
            Destroy(gameObject);
        }
    }
}