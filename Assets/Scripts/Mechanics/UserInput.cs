using Common;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechanics
{
    public class UserInput : MonoBehaviour
    {
        private InputAction moving;
        private InputAction mousePoint;
        private Vector2 direction;
        private IControllable controllable;

        private void Start()
        {
            controllable = GetComponent<IControllable>();
            moving = InputManager.Instance.InputControl.Player.Movement;
        }

        private void Update()
        {
            direction = moving.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            controllable.Move(direction);
        }
    }
}