using Entity.Player;
using UnityEngine;

namespace Entity.Enemy
{
    public class AI : EntityController
    {
        private InputController playerController;
        private void Start()
        {
            playerController = FindObjectOfType<InputController>();
        }

        private void FixedUpdate()
        {
            Vector3 direction = playerController.transform.position - transform.position;
            Move(direction, Time.fixedDeltaTime);
        }
    }
}
