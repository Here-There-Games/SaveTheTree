using System.Collections.Generic;
using System.Linq;
using Common;
using Interfaces;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    public class Tree : MonoBehaviour, IDead
    {
        private static readonly int outline = Shader.PropertyToID("_Outline");
        private const string HEALTH_ATTRIBUTE = "Health";

        public event UnityAction OnDiedEvent;
        public event UnityAction<TreeStage> ChangeStageEvent;

        [SerializeField] private List<TreeStage> stages;

        private StatHandle statHandle;
        private TreeStage currentStage;
        private new SpriteRenderer renderer;
        private GameManager gameManager;

        private void Awake()
        {
            statHandle = GetComponent<StatHandle>();
            renderer = GetComponent<SpriteRenderer>();
            gameManager = GameManager.Instance;
            
            renderer.material.SetFloat(outline, 1);
            EntityLevel level = statHandle.Level;
            level.LevelUpEvent += ChangeState;
            ChangeState(level.Level);
        }

        private void UpdateStage()
        {
            renderer.sprite = currentStage.Sprite;
            statHandle.GetAttribute(HEALTH_ATTRIBUTE).AddToMaxValue(currentStage.AddHP);
            ChangeStageEvent?.Invoke(currentStage);
        }

        private void ChangeState(int newLevel)
        {
            if(currentStage != null){
                statHandle.GetAttribute(HEALTH_ATTRIBUTE).AddToMaxValue(-currentStage.AddHP);
            }

            foreach(TreeStage treeStage in stages.Where(treeStage => treeStage.NeedLvl == newLevel)){
                currentStage = treeStage;
                UpdateStage();
                return;
            }
        }

        public void Dead()
        {
            gameManager.UpdateGameState(GameState.Paused);
            OnDiedEvent?.Invoke();
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public class TreeStage
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int NeedLvl { get; private set; }
        [field: SerializeField] public float AddHP { get; private set; }
    }
}