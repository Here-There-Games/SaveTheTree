using Mechanics;
using UnityEngine;

namespace Entity
{
    public class Cactus : EnemyAI
    {
        [SerializeField] private EntityAttackRange attack;

        private new Rigidbody2D rigidbody;
        private Transform enemyTransform;
        private Transform treeTransform;
        private Animator animator;

        protected override void InitializeAwake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            treeTransform = tree.transform;
            enemyTransform = transform;

            attack.Init(this);
        }

        protected override Vector2 GetDirection()
        {
            Vector2 direction = treeTransform.position - enemyTransform.position;

            if(Vector3.Distance(rigidbody.position, treeTransform.position) <= attack.Range){
                if(attack.TryAttack(treeTransform.position - attack.Point.position)){
                    // TODO додати якісь ефекти
                }

                return Vector2.zero;
            }

            return direction;
        }
    }
}