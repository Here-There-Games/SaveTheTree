using Common;
using UnityEngine;

namespace Mechanics
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject entity;
        [SerializeField] private float cooldown;

        private Timer timer;

        private void Awake()
        {
            timer = new Timer(this, cooldown);
            timer.OnEnd += timer.Start;
            timer.OnStart += Spawn;
            timer.Start();            
        }

        private void Spawn() 
            => Instantiate(entity, transform.position, Quaternion.identity);
    }
}