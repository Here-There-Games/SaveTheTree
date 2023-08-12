using Common;
using Interfaces;
using UI;
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
        [SerializeField] private Item waterDrop;

        private Timer timer;
        private StatHandle stat;
        private StatHandle statTree;

        private void Awake()
        {
            stat = GetComponent<StatHandle>();
            statTree = FindObjectOfType<Tree>().GetComponent<StatHandle>();
            timer = new Timer(this, cooldownToRespawn);
            timer.StartEvent += StartRespawn;
            timer.EndEvent += EndRespawn;
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

        private void StartRespawn()
        {
            Instantiate(waterDrop, transform.position, Quaternion.identity).SetExperience(experience);
            InputManager.Instance.InputControl.Player.Disable();
            gameObject.SetActive(false);
        }

        private void EndRespawn()
        {
            transform.position = positionToRespawn.position;
            InputManager.Instance.InputControl.Player.Enable();
            gameObject.SetActive(true);
            stat.Health.Add(stat.Health.MaxValue);
        }
    }
}