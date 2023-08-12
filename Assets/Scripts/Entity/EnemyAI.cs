using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    public abstract class EnemyAI : MonoBehaviour, IDead
    {
        public event UnityAction DiedEvent;
        
        [field: SerializeField] protected Item item { get; private set; }
        
        [SerializeField] private float experience;

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
            DiedEvent?.Invoke();
            Item instance = Instantiate(item, transform.position, Quaternion.identity);
            instance.SetExperience(experience);
            Destroy(gameObject);
        }

        protected abstract void InitializeAwake();

        protected abstract Vector2 GetDirection();
    }
}