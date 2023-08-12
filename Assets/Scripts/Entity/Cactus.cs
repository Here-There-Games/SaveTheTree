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
        private bool spawned { get; } = false;

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
            if(!spawned)
                return Vector2.zero;
            Vector2 direction = treeTransform.position - enemyTransform.position;

            if(Vector3.Distance(rigidbody.position, treeTransform.position) <= attack.Range){
                if(attack.TryAttack()){
                    Vector3 targetDirection = treeTransform.position - attack.Point.position;
                    Projective projective = Instantiate(attack.Projective, attack.Point.position, Quaternion.identity);
                    projective.InitBullet(targetDirection,attack,attack.Layer);
                }

                return Vector2.zero;
            }

            return direction;
        }
    }
}