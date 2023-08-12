using UnityEngine;
using UnityEngine.Events;

namespace UI.Menu
{
    public class Outline : MonoBehaviour
    {
        public event UnityAction ClickEvent;

        private static readonly int outline = Shader.PropertyToID("_Outline");
        private static readonly int color1 = Shader.PropertyToID("_Color");

        [SerializeField] private Color color;

        private Material material;

        private void Awake()
        {
            material = GetComponent<SpriteRenderer>().material;
            material.SetColor(color1, color);
        }

        private void ChangeOutline(bool visible)
        {
            material.SetFloat(outline, visible ? 1 : 0);
        }

        private void OnMouseEnter()
        {
            ChangeOutline(true);
        }

        private void OnMouseExit()
        {
            ChangeOutline(false);
        }

        private void OnMouseDown()
        {
            ClickEvent?.Invoke();
        }
    }
}