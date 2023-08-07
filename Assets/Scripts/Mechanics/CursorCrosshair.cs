using UnityEngine;
using UnityEngine.EventSystems;

namespace Mechanics
{
    public class CursorCrosshair : MonoBehaviour, IPointerMoveHandler
    {
        [SerializeField] private Texture2D cursorTexture;
        [SerializeField] private Vector2 cursorOffset;
        
        [ContextMenu("Update Cursor")]
        private void SetCursor()
        {
            Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.Auto);            
        }
    
        public void OnPointerMove(PointerEventData eventData)
        {
            print(eventData.delta);
        }
    }
}