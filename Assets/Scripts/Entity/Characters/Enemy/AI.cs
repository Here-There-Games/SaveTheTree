using Entity.Characters.Player;
using UnityEngine;

namespace Entity.Characters.Enemy
{
    public class AI : EntityController
    {
        private InputController playerController;
        protected override void Start()
        {
            base.Start();
            playerController = FindObjectOfType<InputController>();
        }

        private void FixedUpdate()
        {
            Vector3 direction = playerController.transform.position - transform.position;
            Move(direction, Time.fixedDeltaTime);
        }
    }
}
