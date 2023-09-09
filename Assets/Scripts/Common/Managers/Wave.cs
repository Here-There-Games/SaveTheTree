using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Common
{
    public class Wave
    {
        private readonly Dictionary<EnemyAI, int> enemies;

        public Wave(Dictionary<EnemyAI, int> enemies)
            => this.enemies = enemies;

        public EnemyAI Spawn(EnemyAI enemy, Vector3 position, Quaternion rotation)
        {
            EnemyAI enemyAI = Object.Instantiate(enemy, position, rotation);
            enemyAI.GetComponent<SpriteRenderer>().sortingOrder = 2;

            if(enemies.TryGetValue(enemy, out int value))
                enemies[enemy] = value - 1;

            return enemyAI;
        }

        public bool CanSpawn(EnemyAI enemy)
            => enemies.TryGetValue(enemy, out int value) && value > 0;
    }
}