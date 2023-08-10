using System.Collections.Generic;
using Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common
{
    public class SpawnerManager : BaseSingleton<SpawnerManager>
    {
        public int WaveCount { get; private set; }

        [SerializeField] private Vector2 cameraOffset = new(15, 10); // 9,5 camera
        [SerializeField] private EnemyAI[] enemiesCanSpawn;
        [SerializeField] private float cooldown = 2;
        [SerializeField] private float cooldownWhenEndWave = 3;

        private Wave currentWave;
        private Timer timer;
        private Timer waveTimer;
        private Camera mainCamera;
        private EnemyAI enemyForCurrentSpawn;
        private Transform player;
        private List<EnemyAI> enemiesSpawned;

        protected override void Initialize()
        {
            player = FindObjectOfType<Player>().transform;
            mainCamera = Camera.main;

            currentWave = GenerateWaveDict();
            timer = new Timer(this, cooldown);
            timer.OnStart += StartCooldown;
            timer.OnEnd += EndCooldown;
            
            waveTimer = new Timer(this, cooldownWhenEndWave);
            waveTimer.OnEnd += () =>
                                   {
                                       currentWave = GenerateWaveDict();
                                       timer.Start();
                                   };
            
            enemiesSpawned = new List<EnemyAI>();
            
            timer.Start();
        }

        private void StartCooldown()
        {
            enemyForCurrentSpawn = GetEnemyForSpawn();

            if(currentWave.CanSpawn(enemyForCurrentSpawn)){
                EnemyAI spawned = currentWave.Spawn(enemyForCurrentSpawn, GetPositionForSpawn(), Quaternion.identity);
                spawned.DiedEvent += CheckEnemyDie;
                enemiesSpawned.Add(spawned);
            }
        }

        private void EndCooldown()
        {
            if(!currentWave.CanSpawn(enemyForCurrentSpawn))
                return;

            timer.Start();
        }

        private void CheckEnemyDie()
        {
            if(enemiesSpawned.Count - 1 == 0)
                waveTimer.Start();
            enemiesSpawned.RemoveAt(0);
        }

        private EnemyAI GetEnemyForSpawn()
        {
            return enemiesCanSpawn[Random.Range(0, enemiesCanSpawn.Length)];
        }
        
        private Wave GenerateWaveDict()
        {
            Dictionary<EnemyAI, int> enemies = new Dictionary<EnemyAI, int>{
                { enemiesCanSpawn[0], Random.Range(1, 10) },
                { enemiesCanSpawn[1], Random.Range(1, 10) }
            };
            Wave newWave = new Wave(enemies);
            WaveCount++;
            return newWave;
        }

        private Vector3 GetPositionForSpawn()
        {
            Vector3 randomPosition = GetRandomPosition();
            while(InCameraFOV(randomPosition))
                randomPosition = GetRandomPosition();
            return randomPosition;
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 position = player.transform.position;
            float randomX = Random.Range(position.x - cameraOffset.x, position.x + cameraOffset.x);
            float randomY = Random.Range(position.y - cameraOffset.y, position.y + cameraOffset.y);
            return new Vector3(randomX, randomY, 0);
        }

        private bool InCameraFOV(Vector3 position)
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(position);
            return viewportPoint is{ z: > 0, x: > 0 and < 1, y: > 0 and < 1 };
        }
    }

    [System.Serializable]
    public class Wave
    {
        private Dictionary<EnemyAI, int> enemies;

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