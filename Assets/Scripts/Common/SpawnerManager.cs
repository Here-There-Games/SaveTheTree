using Entity;
using UnityEngine;
using Tree = Entity.Tree;

namespace Common
{
    public class SpawnerManager : MonoBehaviour
    {
        [SerializeField] private EnemyAITumbleweed enemy;
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
            Vector3 randomPosition = GetRandomPosition();

            while(InCameraFOV(randomPosition))
                randomPosition = GetRandomPosition();

            EnemyAITumbleweed enemyAICactus = Instantiate(enemy, randomPosition, Quaternion.identity);
            enemyAICactus.GetComponent<SpriteRenderer>().sortingOrder = 2;
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