using System;
using Common;
using UnityEngine;

namespace UI.Menu
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Panel : MonoBehaviour
    {
        [SerializeField] private bool showInStart;
        [SerializeField] private bool showInEnd;
        [SerializeField] private bool showInPaused;
        [SerializeField] private bool showInResume;
        [SerializeField] private GameState state;
        
        private CanvasGroup canvasGroup;
        private GameManager gameManager;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            gameManager = GameManager.Instance;
            if(showInStart)
                gameManager.OnGameStartEvent += () =>
                                                    {
                                                        Visible(true);
                                                    };
        }
        
        protected void Visible(bool visible)
        {
            canvasGroup.alpha = visible ? 1 : 0;
            canvasGroup.blocksRaycasts = visible;
            canvasGroup.interactable = visible;
        }
    }
}