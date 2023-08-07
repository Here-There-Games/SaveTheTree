namespace Common
{
    public class InputManager : BaseSingleton<InputManager>
    {
        public ControllInput player { get; private set; }

        protected override void Initialize()
        {
            player = new ControllInput();
        }

        private void OnEnable()
        {
            player.Enable();
        }

        private void OnDestroy()
        {
            player.Disable();
        }
    }
}