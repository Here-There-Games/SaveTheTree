using System;

namespace Common
{
    public class InputManager : BaseSingleton<InputManager>
    {
        public ControllInput InputControl { get; private set; }

        protected override void Initialize()
        {
            InputControl = new ControllInput();
            InputControl.Enable();            
        }

        private void OnDisable()
        {
            InputControl.Disable();
        }

        private void OnEnable()
        {
            InputControl.Enable();
        }

        private void OnDestroy()
        {
            InputControl.Disable();
        }
    }
}