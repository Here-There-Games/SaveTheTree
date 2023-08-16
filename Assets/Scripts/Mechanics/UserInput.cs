using Common;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechanics
{
    public class UserInput : MonoBehaviour
    {
        private Camera mainCamera;
        private InputAction moving;
        private InputAction fire;
        private Vector2 direction;
        private IControllable controllable;
        private IWeapon weapon;

        private void Awake()
        {
            weapon = GetComponentInChildren<IWeapon>();
            controllable = GetComponent<IControllable>();
        }

        private void Start()
        {
            mainCamera = Camera.main;
            fire = InputManager.Instance.InputControl.Player.Fire;
            moving = InputManager.Instance.InputControl.Player.Movement;
            Subscribe();
        }

        private void Update()
        {
            direction = moving.ReadValue<Vector2>();
            if(weapon.CanRotate && Time.timeScale != 0)
                weapon.RotateWeapon(CalculateRotateForWeapon());
        }

        private void FixedUpdate()
        {
            controllable.Move(direction);
        }

        private void Subscribe()
        {
            fire.performed += FireOnPerformed;
        }

        private void FireOnPerformed(InputAction.CallbackContext context)
        {
            if(context.performed && weapon != null)
                weapon.Attack(CalculateRotateForWeapon());
            else
                Debug.LogWarning($"Weapon in {GetType().Name}");
        }

        private Vector2 CalculateRotateForWeapon()
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            return mousePos - (Vector2)transform.position;
        }
    }
}