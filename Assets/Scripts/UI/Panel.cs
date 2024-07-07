using Common;
using Common.Managers;
using UnityEngine;

namespace UI.Menu
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Panel : MonoBehaviour
    {
        [SerializeField] private bool setNewState = true;

        private CanvasGroup canvasGroup;
        private GameManager gameManager;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            gameManager = GameManager.Instance;
        }

        [ContextMenu("Close")]
        public void Close()
            => Close(GameState.Playing);
        
        [ContextMenu("Show")]
        public void Show()
            => Show(GameState.Playing);
        
        public void Show(GameState state)
            => Visible(true, state);

        public void Close(GameState state)
            => Visible(false, state);

        private void Visible(bool visible, GameState state)
        {
            canvasGroup.alpha = visible ? 1 : 0;
            canvasGroup.blocksRaycasts = visible;
            canvasGroup.interactable = visible;
            if(setNewState)
                gameManager.UpdateGameState(state);
        }
    }
}