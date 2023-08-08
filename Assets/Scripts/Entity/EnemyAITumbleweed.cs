using Common;
using Common.Utilities;
using Interfaces;
using UnityEngine;

namespace Entity
{
    public class EnemyAITumbleweed : MonoBehaviour, IFloat, IDead
    {
        public float Value => experience;

        [SerializeField] private TumbleweedAttack attack;
        [SerializeField] private float experience;

        private Transform enemyTransform;
        private Transform treeTransform;
        private Tree tree;
        private IControllable controllable;

        private void Awake()
        {
            controllable = GetComponent<IControllable>();
            // StatHandle stat = GetComponent<StatHandle>();
            tree = FindObjectOfType<Tree>();

            Extensions.CheckForNullComponents(this, new[]{ /*stat,*/ tree, (Component)controllable });

            treeTransform = tree.transform;
            enemyTransform = transform;

            attack.Init(this);
        }

        private void FixedUpdate()
        {
            Vector2 direction = treeTransform.position - enemyTransform.position;
            controllable.Move(attack.Attacked ? -direction : direction);
        }

        /*
        private void OnTriggerExit2D(Collider2D other)
        {
            throw new NotImplementedException();
        }*/

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.layer == attack.Layer){
                // tree.GetComponent<ITakeDamage>().TakeDamage(attack);
                attack.TryAttack(col);
                print("attack");
            }
        }

        public void Dead()
        {
            tree.GetComponent<StatHandle>().Level.AddExperience(this);
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class TumbleweedAttack : IDamage
    {
        public bool Attacked { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public LayerMask Layer { get; private set; }

        [SerializeField] private float cooldown;
        
        private Timer timer;

        public void Init(MonoBehaviour mono)
        {
            timer = new Timer(mono, cooldown);
            timer.OnStart += () => Attacked = true;
            timer.OnEnd += () => Attacked = false;
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
        }
    }
}