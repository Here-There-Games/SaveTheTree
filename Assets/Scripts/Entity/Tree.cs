using System.Collections.Generic;
using System.Linq;
using Common.Utilities;
using Mechanics;
using UnityEngine;

namespace Entity
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private List<TreeStage> stages;

        private StatHandle statHandle;
        private TreeStage currentStage;
        private new SpriteRenderer renderer;

        private void Awake()
        {
            statHandle = GetComponent<StatHandle>();
            renderer = GetComponent<SpriteRenderer>();
            
            Extensions.CheckForNullComponents(this, new Component[]{ renderer, statHandle});

            EntityLevel level = statHandle.Level;
            
            level.OnLevelUp += OnChangeState;
            OnChangeState(level.Level);
        }
        private void UpdateStage()
        {
            renderer.sprite = currentStage.Sprite;
            statHandle.Health.UpgradeAttribute(currentStage.AddHP);
        }
        
        private void OnChangeState(int newLevel)
        {
            if(currentStage != null){
                statHandle.Health.UpgradeAttribute(-currentStage.AddHP);
            }

            foreach(TreeStage treeStage in stages.Where(treeStage => treeStage.NeedLvl == newLevel)){
                currentStage = treeStage;
                UpdateStage();
                return;
            }
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