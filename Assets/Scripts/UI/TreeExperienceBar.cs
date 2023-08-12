using Entity;
using UnityEngine;
using UnityEngine.UI;
using Tree = Entity.Tree;

namespace UI
{
    public class TreeExperienceBar : MonoBehaviour
    {
        [SerializeField] private Image image; 

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

            if(treeStat.Level.Level == 0)
                image.enabled = false;
        }
    }
}