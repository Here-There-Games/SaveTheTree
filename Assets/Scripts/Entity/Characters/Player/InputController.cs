using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity.Characters.Player
{
    public class InputController : EntityController
    {
        [Header("Inputs")]
        [SerializeField] private InputActionReference moveReference;
        [SerializeField] private InputActionReference attackReference;

        private WateringCan wateringCan;

        protected override void Start()
        {
            base.Start();

            wateringCan = GetComponentInChildren<WateringCan>();

            ChangeInputEnabled(true);
            attackReference.action.started += OnAttackPressStarted;
        }

        private void OnAttackPressStarted(InputAction.CallbackContext obj)
        {
            if(wateringCan != null){
                wateringCan.Attack(GetWeaponDirection());
            }
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

            if(wateringCan != null){
                wateringCan.Rotate(GetWeaponDirection());
            }
        }

        private Vector2 GetWeaponDirection()
        {
            Vector3 mousePosition = Camera.main!.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 thisPosition = transform.position;
            return new Vector2(mousePosition.x - thisPosition.x, mousePosition.y - thisPosition.y);
        }
    }
}