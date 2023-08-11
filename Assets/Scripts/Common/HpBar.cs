using System;
using System.Collections.Generic;
using Entity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Common
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Sprite fullHp;
        [SerializeField] private Sprite halfHp;
        [SerializeField] private Sprite emptyHp;
        [SerializeField] private List<Image> images;

        private void Awake()
        {
            Player player = FindObjectOfType<Player>();
            StatHandle playerStat = player.GetComponent<StatHandle>();
            playerStat.Health.OnChangeValue += UpdateHpBar;
        }

        private void UpdateHpBar(float value)
        {
            int newValue = (int)value;

            images[0].sprite = newValue switch{
                0 => emptyHp,
                1 => halfHp,
                _ => fullHp
            };

            images[1].sprite = newValue < 3 ? emptyHp : newValue == 3 ? halfHp : fullHp;
            images[2].sprite = newValue < 5 ? emptyHp : newValue == 5 ? halfHp : fullHp;
        }
    }
}