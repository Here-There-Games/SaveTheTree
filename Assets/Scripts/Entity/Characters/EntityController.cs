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
            if(!statHandler.CurrentStats.TryGetStat(STAT_NAME, out IStat speed))
                throw new NullReferenceException($"{name} is not founded Stat ()");
            float x = transform.position.x + direction.normalized.x * speed.Value * delta;
            float y = transform.position.y + direction.normalized.y * speed.Value * delta;
            transform.position = new Vector2(x, y);
        }
    }
}