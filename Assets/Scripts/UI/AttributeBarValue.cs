using Entity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Tree = Entity.Tree;

namespace UI
{
    public class AttributeBarValue : MonoBehaviour
    {
        private const string HEALTH_ATTRIBUTE = "Health";

        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;

        private void Awake()
        {
            StatHandle stat = FindObjectOfType<Tree>().GetComponent<StatHandle>();
            stat.GetAttribute(HEALTH_ATTRIBUTE).ChangeValueNormalizedEvent += UpdateHp;
            UpdateHp(stat.GetAttribute(HEALTH_ATTRIBUTE).Value / stat.GetAttribute(HEALTH_ATTRIBUTE).MaxValue);
        }

        private void UpdateHp(float value)
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