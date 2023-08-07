using System;
using Interfaces;
using Mechanics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entity
{
    public class EnemyAI : MonoBehaviour, ITakeDamage
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
            health.OnZeroValue += Dead;
        }

        private void Dead()
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            Vector2 direction = treeTransform.position - enemyTransform.position;
            if(Vector3.Distance(rigidbody.position, treeTransform.position) <= attack.Range){
                controllable.Move(Vector2.zero);

                if(attack.TryAttack()){
                    RaycastHit2D hit = Physics2D.Raycast(enemyTransform.position, direction, attack.Range);
                    hit.collider.GetComponent<ITakeDamage>()?.TakeDamage(attack);
                }
            }
            else{
                controllable.Move(direction);
            }
        }

        public void TakeDamage(IDamage damageI)
        {
            health.Spend(damageI.Damage);
        }
    }
}