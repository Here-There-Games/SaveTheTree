using UnityEngine;

namespace Mechanics
{
    [System.Serializable]
    public class EntityAttackRange : EntityAttack
    {
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public Transform Point { get; private set; }
    }
}