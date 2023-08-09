using Entity;
using UnityEngine;
using Tree = Entity.Tree;

namespace Common
{
    public class SpawnerManager : MonoBehaviour
    {
        [SerializeField] private EnemyAI enemy1;
        [SerializeField] private EnemyAI enemy2;
        [SerializeField] private float cooldown;
        [SerializeField] private Vector2 cameraOffset = new(15, 10); // 9,5 camera

        private Camera mainCamera;
        private Timer timer;
        private Transform player;
        private Tree tree;

        private void Awake()
        {
            player = FindObjectOfType<Player>().transform;
            tree = FindObjectOfType<Tree>();
            mainCamera = Camera.main;
            timer = new Timer(this, cooldown);
            timer.OnEnd += timer.Start;
            timer.OnStart += Spawn;
            timer.Start();
        }

        private void Spawn()
        {
            if(tree is null)
                return;
            Spawn(enemy1,GetPositionForSpawn(),Quaternion.identity);
            Spawn(enemy2,GetPositionForSpawn(),Quaternion.identity);
        }

        private static EnemyAI Spawn(EnemyAI enemy, Vector3 position, Quaternion rotation)
        {
            EnemyAI enemyAI = Instantiate(enemy, position, rotation);
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
}