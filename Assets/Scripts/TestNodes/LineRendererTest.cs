using UnityEngine;
using UnityEngine.EventSystems;

namespace TestNodes
{
    public class LineRendererTest : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public LineRenderer lineRenderer;
        public Sprite customSprite;

        private int countLines;
        private Vector3 startPointPosition;
        private Vector3 endPointPosition;
        private bool drawing;

        private void Start()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default")){
                mainTexture = customSprite.texture,
                // mainTextureOffset = customSprite.rect.position,
                // mainTextureScale = customSprite.rect.size
            };
            // lineRenderer.
        }

        public void Connect(Vector3 startPosition, Vector3 endPosition)
        {
            lineRenderer.SetPosition(countLines, startPosition);
            countLines++;
            lineRenderer.SetPosition(countLines, endPosition);
            countLines++;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            endPointPosition = eventData.position;

            if(startPointPosition != Vector3.zero || endPointPosition != Vector3.zero){
                Connect(startPointPosition, endPointPosition);
                startPointPosition = Vector3.zero;
                endPointPosition = Vector3.zero;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            startPointPosition = eventData.position;
        }
    }
}