using UnityEngine;

namespace Mechanics
{
    [System.Serializable]
    public class EntityAttackRange : EntityAttack
    {
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public Transform Point { get; private set; }
        [field: SerializeField] public Projective Projective { get; private set; }

        protected override void Attack(Vector3 targetDirection)
        {
            if(Projective == null)
                return;
            
            Projective projective = Object.Instantiate(Projective, Point.position, Quaternion.identity);
            projective.InitBullet(targetDirection, this);
        }
    }
}