using Entity;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Tree = Entity.Tree;

namespace UI
{
    public class TreeExperienceBar : MonoBehaviour
    {
        [SerializeField] private Image image; 
        [SerializeField] private TMP_Text text; 

        private void Awake()
        {
            Tree tree = FindObjectOfType<Tree>();
            StatHandle treeStat = tree.GetComponent<StatHandle>();
            tree.ChangeStageEvent += stage =>
                                         {
                                             image.enabled = true;
                                             image.sprite = stage.Sprite;
                                         }; 
            treeStat.Level.ChangeExperienceEvent += (current, max) => image.fillAmount = current / max;
            treeStat.Level.LevelUpEvent += level => text.text = level.ToString("#");

            if(treeStat.Level.Level == 0)
                image.enabled = false;
        }
    }
}