using Mechanics;
using UnityEngine;

namespace Entity
{
    public class EnemyAICactus : EnemyAI
    {
        [SerializeField] private EntityAttackRange attack;

        private new Rigidbody2D rigidbody;
        private Transform enemyTransform;
        private Transform treeTransform;

        protected override void InitializeAwake()
        {
            rigidbody = GetComponent<Rigidbody2D>();

            treeTransform = tree.transform;
            enemyTransform = transform;

            attack.Init(this);
        }

        protected override Vector2 GetDirection()
        {
            Vector2 direction = treeTransform.position - enemyTransform.position;

            if(Vector3.Distance(rigidbody.position, treeTransform.position) <= attack.Range){
                if(attack.CanAttack){
                    RaycastHit2D hit =
                        Physics2D.Raycast(enemyTransform.position, direction, attack.Range, attack.Layer);

                    if(hit.collider != null)
                        attack.TryAttack(hit.collider);
                }

                return Vector2.zero;
            }

            return direction;
        }
    }
}