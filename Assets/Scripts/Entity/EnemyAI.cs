using System;
using Common;
using Interfaces;
using Mechanics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entity
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EntityAttribute health;
        [SerializeField] private EntityAttack attack;

        private new Rigidbody2D rigidbody;
        private Transform enemyTransform;
        private Transform treeTransform;
        private Tree tree;
        private IControllable controllable;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            controllable = GetComponent<IControllable>();
            tree = FindObjectOfType<Tree>();

            if(controllable == null)
                throw new NullReferenceException($"Add to {name} a class who realise IControllable interface");

            treeTransform = tree.transform;
            enemyTransform = transform;

            attack.Init(this);
        }

        private void FixedUpdate()
        {
            Vector2 direction = treeTransform.position - enemyTransform.position;
            if(Vector3.Distance(rigidbody.position, treeTransform.position) <= attack.Radius){
                controllable.Move(Vector2.zero);

                if(attack.TryAttack()){
                    RaycastHit2D hit = Physics2D.Raycast(enemyTransform.position, direction);
                    hit.collider.GetComponent<ITakeDamage>()?.TakeDamage(attack);
                }
            }
            else{
                controllable.Move(direction);
            }
        }
    }

    [Serializable]
    public class EntityAttack: IDamage
    {
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        
        [SerializeField] private float cooldown;

        private Timer timer;

        public void Init(MonoBehaviour mono)
        {
            timer = new Timer(mono, cooldown);
            timer.OnStart += Attack;
        }

        public bool TryAttack()
        {
            if(timer.Stopped){
                timer.Start();
                return true;
            }

            return false;
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }
    }
}