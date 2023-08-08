using Common.Utilities;
using Interfaces;
using Mechanics;
using UnityEngine;

namespace Entity
{
    public class EnemyAICactus : MonoBehaviour, IDead, IFloat
    {
        public float Value => experience;

        [SerializeField] private EntityAttack attack;
        [SerializeField] private float experience;

        private new Rigidbody2D rigidbody;
        private StatHandle stat;
        private Transform enemyTransform;
        private Transform treeTransform;
        private Tree tree;
        private IControllable controllable;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            controllable = GetComponent<IControllable>();
            stat = GetComponent<StatHandle>();
            tree = FindObjectOfType<Tree>();

            Extensions.CheckForNullComponents(this, new[]{ rigidbody, stat, tree, (Component)controllable });

            treeTransform = tree.transform;
            enemyTransform = transform;

            attack.Init(this);
            stat.Health.OnChangeValue += value =>
                                             {
                                                 if(value <= 0)
                                                     Dead();
                                             };
        }

        private void FixedUpdate()
        {
            if(treeTransform == null)
                return;
            Vector2 direction = treeTransform.position - enemyTransform.position;

            if(Vector3.Distance(rigidbody.position, treeTransform.position) <= attack.Range){
                controllable.Move(Vector2.zero);

                if(attack.Can){
                    RaycastHit2D hit =
                        Physics2D.Raycast(enemyTransform.position, direction, attack.Range, attack.Layer);
                    if(hit.collider != null && attack.TryAttack(hit.collider)){
                        hit.collider.GetComponent<ITakeDamage>()?.TakeDamage(attack);
                    }
                }
            }
            else{
                controllable.Move(direction);
            }
        }

        public void TakeDamage(IDamage damageI)
        {
            stat.Health.Spend(damageI.Damage);
        }

        public void Dead()
        {
            tree.GetComponent<StatHandle>().Level.AddExperience(this);
            Destroy(gameObject);
        }
    }
}