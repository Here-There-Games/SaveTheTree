using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Player
{
    public class InputController : EntityController
    {
        [Header("Inputs")]
        [SerializeField] private InputActionReference moveReference;
        [SerializeField] private InputActionReference attackReference;

        private void Start()
        {
            ChangeInputEnabled(true);
            attackReference.action.started += OnAttackPressStarted;
        }

        private void OnAttackPressStarted(InputAction.CallbackContext obj)
        {
            Debug.Log("Attack");
        }

        public void ChangeInputEnabled(bool enable)
        {
            if(enable){
                moveReference.action.Enable();
                attackReference.action.Enable();
            }
            else{
                moveReference.action.Disable();
                attackReference.action.Disable();
            }
        }

        private void FixedUpdate()
        {
            Move(moveReference.action.ReadValue<Vector2>(), Time.fixedDeltaTime);
        }
    }
}