﻿using System.Collections.Generic;
using System.Linq;
using Common.Utilities;
using Interfaces;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    public class Tree : MonoBehaviour, IDead
    {
        public event UnityAction DiedEvent;
        
        [SerializeField] private List<TreeStage> stages;

        private StatHandle statHandle;
        private TreeStage currentStage;
        private new SpriteRenderer renderer;

        private void Awake()
        {
            statHandle = GetComponent<StatHandle>();
            renderer = GetComponent<SpriteRenderer>();

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

        public void Dead()
        {
            Time.timeScale = 0;
            DiedEvent?.Invoke();
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