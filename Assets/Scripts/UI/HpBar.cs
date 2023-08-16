using UnityEngine;

namespace UI
{
    public abstract class HpBar : MonoBehaviour
    {
        protected abstract void UpdateHp(float value);
    }
}