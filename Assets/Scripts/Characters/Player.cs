using Common;
using Interfaces;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    public class Player : MonoBehaviour, IDead
    {
        public event UnityAction DiedEvent;

        [SerializeField] private float cooldownToRespawn;
        [SerializeField] private Transform positionToRespawn;

        private Timer timer;
        private StatHandle stat;
        private StatHandle statTree;

        private void Awake()
        {
            stat = GetComponent<StatHandle>();
            statTree = FindObjectOfType<Tree>().GetComponent<StatHandle>();
            timer = new Timer(this, cooldownToRespawn);
            timer.OnStartEvent += OnStartRespawn;
            timer.OnEndEvent += OnEndRespawn;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Item item = other.GetComponent<Item>();

            if(item != null){
                statTree.Level.AddExperience(item);
                item.Pickup();
            }
        }

        public void Dead()
        {
            GlobalProgressBar.Instance.ShowProgressBar("Respawning", timer);
            DiedEvent?.Invoke();
        }

        private void OnStartRespawn()
        {
            InputManager.Instance.InputControl.Player.Disable();
            gameObject.SetActive(false);
        }

        private void OnEndRespawn()
        {
            transform.position = positionToRespawn.position;
            InputManager.Instance.InputControl.Player.Enable();
            gameObject.SetActive(true);
            stat.StatData.FindAttribute("Health").RestoreAttribute();
        }
    }
}