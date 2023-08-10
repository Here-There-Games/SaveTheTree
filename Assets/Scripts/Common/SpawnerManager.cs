using System;
using Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common
{
    public class SpawnerManager : MonoBehaviour
    {
        [SerializeField] private Vector2 cameraOffset = new(15, 10); // 9,5 camera
        [SerializeField] private Wave[] waves;
        [SerializeField] private Wave currentWave;
        [SerializeField] private EnemyAI[] enemiesCanSpawn;

        private Camera mainCamera;
        private Transform player;
        private bool needGenerateWave;

        private void Awake()
        {
            player = FindObjectOfType<Player>().transform;
            mainCamera = Camera.main;

            if(waves.Length > 0)
                currentWave = waves[0];
            else
                needGenerateWave = true;

            currentWave.Initialize(this);
            currentWave.TimerSpawning.OnEnd += EndCooldown;
            currentWave.TimerSpawning.OnStart += StartCooldown;
            currentWave.TimerSpawning.Start();
        }

        private void StartCooldown()
        {
            if(!currentWave.CanSpawn()){
                if(needGenerateWave){
                    EnemyAI enemyForWave = enemiesCanSpawn[Random.Range(0, enemiesCanSpawn.Length)];
                    Wave newWave = new Wave(enemyForWave, Random.Range(1, 5), 1, this);
                    newWave.Subscribe(StartCooldown, EndCooldown);
                    currentWave = newWave;
                }
            }

            Spawn(currentWave.Enemy, GetPositionForSpawn(), Quaternion.identity);
        }

        private void EndCooldown()
        {
            currentWave.TimerSpawning.Start();
        }

        private static EnemyAI Spawn(EnemyAI enemy, Vector3 position, Quaternion rotation)
        {
            EnemyAI enemyAI = Instantiate(enemy, position, rotation);
            enemyAI.GetComponent<SpriteRenderer>().sortingOrder = 2;
            return enemyAI;
        }

        private static EnemyAI Spawn(Wave wave, Vector3 position, Quaternion rotation)
        {
            EnemyAI enemyAI = Instantiate(wave.Enemy, position, rotation);
            enemyAI.GetComponent<SpriteRenderer>().sortingOrder = 2;
            return enemyAI;
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
        [field: SerializeField] public EnemyAI Enemy { get; private set; }
        public Timer TimerSpawning { get; private set; }

        [SerializeField] private int count;
        [SerializeField] private int cooldown;

        public Wave(EnemyAI enemy, int count, int cooldown, MonoBehaviour mono)
        {
            Enemy = enemy;
            this.count = count;
            this.cooldown = cooldown;
            Initialize(mono);
        }

        public void Subscribe(Action startTimer, Action endTimer)
        {
            TimerSpawning.OnEnd += endTimer;
            TimerSpawning.OnStart += startTimer;
        }

        public void Initialize(MonoBehaviour mono)
        {
            TimerSpawning = new Timer(mono, cooldown);
        }

        public bool CanSpawn()
        {
            bool result = count > 0;
            count -= 1;
            return result;
        }
    }
}