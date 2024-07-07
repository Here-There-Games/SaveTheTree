using System;
using UnityEngine;

namespace Entity
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField] internal EntityControlData entityData;

        public void Move(Vector2 direction, float delta)
        {
            float x = transform.position.x + direction.normalized.x * entityData.Speed * delta;
            float y = transform.position.y + direction.normalized.y * entityData.Speed * delta;
            transform.position = new Vector2(x, y);
        }
    }
}