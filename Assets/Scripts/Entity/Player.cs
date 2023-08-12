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
            timer.StartEvent += StartRespawn;
            timer.EndEvent += EndRespawn;
        }

        public void Dead()
        {
            GlobalProgressBar.Instance.ShowProgressBar("Respawning", timer);
            DiedEvent?.Invoke();
        }

        private void StartRespawn()
        {
            FindObjectOfType<EnemyAI>().GetComponent<StatHandle>().Level.AddExperience(this);
            InputManager.Instance.InputControl.Player.Disable();
            gameObject.SetActive(false);
            // renderer.enabled = false;
        }
        private void EndRespawn()
        {
            transform.position = positionToRespawn.position;
            // renderer.enabled = true;
            InputManager.Instance.InputControl.Player.Enable();
            gameObject.SetActive(true);
            stat.Health.Add(stat.Health.MaxValue);        
        }
    }
}