using UnityEngine;

namespace Entity
{
    [CreateAssetMenu(fileName = "Entity Control Data", menuName = "Entity/Control Data")]
    public class EntityControlData : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; } = 2;
    }
}