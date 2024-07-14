using Core.StatSystem;
using Core.StatSystem.Interfaces;
using System;
using UnityEngine;

namespace Entity.Characters
{
    public class EntityController : MonoBehaviour
    {
        private const string STAT_NAME = "Speed";
        private StatHandler statHandler;

        protected virtual void Start()
        {
            statHandler = GetComponent<StatHandler>();
        }

        public void Move(Vector2 direction, float delta)
        {
            float speedConst = 5;
            IStat speed = default;
            if(statHandler != null && !statHandler.CurrentStats.TryGetStat(STAT_NAME, out speed)){
                Debug.LogWarning($"Stat handler in {name} not founded.");
            }
            float x = transform.position.x + direction.normalized.x * (speed?.Value ?? speedConst) * delta;
            float y = transform.position.y + direction.normalized.y * (speed?.Value ?? speedConst) * delta;
            transform.position = new Vector2(x, y);
        }
    }
}