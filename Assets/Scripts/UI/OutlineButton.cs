using UnityEngine;
using UnityEngine.Events;

namespace UI.Menu
{
    public class OutlineButton : MonoBehaviour
    {
        public UnityEvent ClickEvent;

        private static readonly int outline = Shader.PropertyToID("_Outline");
        private static readonly int color1 = Shader.PropertyToID("_Color");

        [SerializeField] private Color color = Color.white;

        [SerializeField] private SpriteRenderer spriteRenderer;
        private Material material;

        private void Awake()
        {
            material = spriteRenderer != null
                           ? spriteRenderer.material
                           : GetComponentInChildren<SpriteRenderer>().material;
            material.SetColor(color1, color);
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

        private void ChangeOutline(bool visible)
        {
            material.SetFloat(outline, visible ? 1 : 0);
        }
    }
}