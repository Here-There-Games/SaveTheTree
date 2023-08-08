using UnityEngine;

namespace Mechanics
{
    public class CursorCrosshair : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorTexture;
        private Vector2 cursorOffset;
        
        [ContextMenu("Update Cursor")]
        private void SetCursor()
        {
            if(cursorTexture != null){
                cursorOffset = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
                Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.Auto);
            }
        }
    }
}