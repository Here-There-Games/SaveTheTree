using System.Collections.Generic;
using Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common
{
    public class SpawnerManager : MonoBehaviour
    {
        public int WaveCount { get; private set; }

        [SerializeField] private Vector2 cameraOffset = new(15, 10); // 9,5 camera
        [SerializeField] private EnemyAI[] enemiesCanSpawn;
        [SerializeField] private float cooldown;
        [SerializeField] private float cooldownWhenEndWave;

        [SerializeField] private Wave currentWave;

        private Timer timer;
        private Timer waveTimer;
        private Camera mainCamera;
        private EnemyAI enemyForCurrentSpawn;
        private Transform player;
        private List<EnemyAI> enemiesSpawned;

        private void Awake()
        {
            player = FindObjectOfType<Player>().transform;
            mainCamera = Camera.main;

            currentWave = GenerateWaveDict();
            timer = new Timer(this, cooldown);

            timer.OnStart += StartCooldown;
            timer.OnEnd += EndCooldown;
            timer.Start();
            waveTimer = new Timer(this, cooldownWhenEndWave);
            
        }

        private void StartCooldown()
        {
            enemyForCurrentSpawn = enemiesCanSpawn[Random.Range(0, enemiesCanSpawn.Length)];

            if(currentWave.CanSpawn(enemyForCurrentSpawn))
                currentWave.Spawn(enemyForCurrentSpawn, GetPositionForSpawn(), Quaternion.identity);
        }

        private void EndCooldown()
        {
            if(!currentWave.CanSpawn(enemyForCurrentSpawn))
                currentWave = GenerateWaveDict();

            timer.Start();
        }

        private Wave GenerateWaveDict()
        {
            Dictionary<EnemyAI, int> enemies = new Dictionary<EnemyAI, int>{
                { enemiesCanSpawn[0], Random.Range(1, 10) },
                { enemiesCanSpawn[1], Random.Range(1, 10) }
            };
            Wave newWave = new Wave(enemies);
            WaveCount++;
            Debug.Log($"Generate a {WaveCount} wave.");
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
        public Dictionary<EnemyAI, int> Enemies;

        public Wave(Dictionary<EnemyAI, int> enemies) 
            => Enemies = enemies;

        public void Spawn(EnemyAI enemy, Vector3 position, Quaternion rotation)
        {
            EnemyAI enemyAI = Object.Instantiate(enemy, position, rotation);
            enemyAI.GetComponent<SpriteRenderer>().sortingOrder = 2;
            
            if(Enemies.TryGetValue(enemy, out int value))
                Enemies[enemy] = value - 1;
            
            Debug.Log($"You spawn {enemy}, {value}");
        }

        public bool CanSpawn(EnemyAI enemy) 
            => Enemies.TryGetValue(enemy, out int value) && value > 0;
    }
}