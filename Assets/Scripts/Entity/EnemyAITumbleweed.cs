using Mechanics;
using UnityEngine;

namespace Entity
{
    public class EnemyAITumbleweed : EnemyAI
    {
        [SerializeField] private EntityAttack attack;

        private Transform enemyTransform;
        private Transform treeTransform;
        private Vector3 startPoint;

        protected override void InitializeAwake()
        {
            treeTransform = tree.transform;
            enemyTransform = transform;
            startPoint = enemyTransform.position;

            attack.Init(this);
        }

        protected override Vector2 GetDirection()
            => attack.CanAttack
                   ? treeTransform.position - enemyTransform.position
                   : startPoint - enemyTransform.position;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.GetComponent<Tree>() != null){
                attack.TryAttack(col);
            }
        }
    }
}