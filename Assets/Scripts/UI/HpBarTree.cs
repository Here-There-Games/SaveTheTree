using Entity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Tree = Entity.Tree;

namespace UI
{
    public class HpBarTree : HpBar
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;

        private void Awake()
        {
            StatHandle stat = FindObjectOfType<Tree>().GetComponent<StatHandle>();
            stat.Health.ChangeValueNormalizedEvent += UpdateHp;
            UpdateHp(stat.Health.Value / stat.Health.MaxValue);
        }

        protected override void UpdateHp(float value)
        {
            if(image != null)
                image.fillAmount = value;
            else
                Debug.Log($"{name} is not contains Image in {GetType().Name}");
            if(text != null)
                text.text = $"Tree: {value * 100:###}/100%";
            else
                Debug.Log($"{name} is not contains Text in {GetType().Name}");
        }
    }
}