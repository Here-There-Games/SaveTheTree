using System.Collections.Generic;
using Entity;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AttributeBarCount : MonoBehaviour
    {
        [SerializeField] private Sprite fullSprite;
        [SerializeField] private Sprite halfSprite;
        [SerializeField] private Sprite emptySprite;
        [SerializeField] private Vector2 spriteSize = new Vector2(50,50);
        [SerializeField] private int countInSprite = 2;
        [SerializeField] private string tagToFind = "Player";

        private readonly List<Image> images = new List<Image>();

        private void Awake()
        {
            GameObject objectWithTag = GameObject.FindGameObjectWithTag(tagToFind);
            StatHandle stat = objectWithTag.GetComponent<StatHandle>();
            stat.Health.ChangeValueEvent += UpdateHp;

            int maxHp = Mathf.FloorToInt(stat.Health.MaxValue / countInSprite);

            for(int i = 0; i < maxHp; i++){
                Image image = new GameObject($"Hp_{i}").AddComponent<Image>();
                image.transform.SetParent(transform);
                image.transform.localScale = Vector3.one;
                image.rectTransform.anchoredPosition3D = Vector3.zero;
                image.rectTransform.sizeDelta = spriteSize;
                image.sprite = fullSprite;
                images.Add(image);
            }
        }

        private void UpdateHp(float value)
        {
            float heart = value / 2;

            foreach(Image image in images){
                switch(heart){
                    case >= 1:
                        image.sprite = fullSprite;
                        heart--;
                        break;
                    case < 1 and > 0:
                        image.sprite = halfSprite;
                        heart--;
                        break;
                    default:
                        image.sprite = emptySprite;
                        break;
                }
            }
        }
    }
}