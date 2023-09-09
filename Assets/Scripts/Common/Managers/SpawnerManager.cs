using System.Collections.Generic;
using Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Managers
{
    public class SpawnerManager : BaseSingleton<SpawnerManager>
    {
        public event System.Action<int> OnUpdateWaveEvent;

        public int WaveCount { get; private set; }
        public Timer timer { get; private set; }
        public Timer WaveTimer { get; private set; }

        [Header("Setting")][SerializeField] private Vector2 cameraOffset = new(15, 10); // 9,5 camera
        [SerializeField] private EnemyAI[] enemiesCanSpawn;
        [SerializeField] private float cooldown = 5;
        [SerializeField] private float cooldownWhenEndWave = 10;
        [SerializeField] private SpawnerView view;

        private Wave currentWave;
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
            timer.OnStartEvent += OnStartEventCooldown;
            timer.OnEndEvent += OnEndEventCooldown;

            WaveTimer = new Timer(this, cooldownWhenEndWave);
            WaveTimer.OnEndEvent += () =>
                                        {
                                            currentWave = GenerateWaveDict();
                                            timer.Start();
                                            WaveCount++;
                                            OnUpdateWaveEvent?.Invoke(WaveCount);
                                        };

            enemiesSpawned = new List<EnemyAI>();
            OnUpdateWaveEvent?.Invoke(WaveCount);

            WaveTimer.Start();
            view.Start();
        }

        private void OnStartEventCooldown()
        {
            enemyForCurrentSpawn = GetEnemyForSpawn();

            if(currentWave.CanSpawn(enemyForCurrentSpawn)){
                EnemyAI spawned = currentWave.Spawn(enemyForCurrentSpawn, GetPositionForSpawn(), Quaternion.identity);
                spawned.OnDiedEvent += CheckEnemyDie;
                enemiesSpawned.Add(spawned);
            }
        }

        private void OnEndEventCooldown()
        {
            if(!currentWave.CanSpawn(enemyForCurrentSpawn))
                return;

            timer.Start();
        }

        private void CheckEnemyDie()
        {
            enemiesSpawned.RemoveAt(0);

            if(enemiesSpawned.Count < 1){
                WaveTimer.Start();
            }
        }

        private EnemyAI GetEnemyForSpawn()
            => enemiesCanSpawn[Random.Range(0, enemiesCanSpawn.Length)];

        private Wave GenerateWaveDict()
        {
            Dictionary<EnemyAI, int> enemies = new Dictionary<EnemyAI, int>{
                { enemiesCanSpawn[0], WaveCount == 0 ? Random.Range(1, 3) : WaveCount * 2 - 1 },
                { enemiesCanSpawn[1], WaveCount == 0 ? 1 : WaveCount - 1 }
            };
            Wave newWave = new Wave(enemies);
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
}