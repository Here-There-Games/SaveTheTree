using UI.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    [RequireComponent(typeof(Button))]
    public class ButtonForChangeGameState : MonoBehaviour
    {
        [SerializeField] private Panel panel;
        [SerializeField] private ButtonTypeToPanel buttonType;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(ChangeVisiblePanel);
        }

        private void ChangeVisiblePanel()
        {
            switch(buttonType){
                case ButtonTypeToPanel.Show:
                    panel.Show(GameState.Paused);
                    break;
                case ButtonTypeToPanel.Close:
                    panel.Close(GameState.Resumed);
                    break;
            }
        }
    }

    public enum ButtonTypeToPanel
    {
        Show,
        Close,
    }
}