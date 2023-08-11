using Common;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    public class Player : MonoBehaviour, IFloat, IDead
    {
        public event UnityAction DiedEvent;
        public float Value => experience;

        [SerializeField] private float experience;
        [SerializeField] private float cooldownToRespawn;
        [SerializeField] private Transform positionToRespawn;

        private new SpriteRenderer renderer;
        private Timer timer;
        private StatHandle stat;

        private void Awake()
        {
            renderer = GetComponent<SpriteRenderer>();
            stat = GetComponent<StatHandle>();
            
            timer = new Timer(this, cooldownToRespawn);
            timer.EndEvent += Respawn;
        }

        public void Dead()
        {
            FindObjectOfType<EnemyAI>().GetComponent<StatHandle>().Level.AddExperience(this);
            // GlobalProgressBar.Instance.ShowProgressBar("Respawning", cooldownToRespawn, null, Respawn);
            GlobalProgressBar.Instance.ShowProgressBar("Respawning", timer);
            DiedEvent?.Invoke();
            // gameObject.SetActive(false);
            renderer.enabled = false;
        }

        private void Respawn()
        {
            transform.position = positionToRespawn.position;
            renderer.enabled = true;
            stat.Health.Add(stat.Health.MaxValue);        
            // gameObject.SetActive(true);
        }
    }
}