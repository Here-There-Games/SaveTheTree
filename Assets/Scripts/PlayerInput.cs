using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputAction moving;
    private Vector2 direction;
    private IControllable controllable;

    private void Start()
    {
        controllable = GetComponent<IControllable>();
        moving = InputManager.Instance.player.Player.Movement;
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