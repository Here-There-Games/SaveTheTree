using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damage = 1;
        [SerializeField] private float speed = 10;
        [SerializeField] private float lifetime = 5;
        private Vector2 direction;

        private void Awake()
        {
            if(Camera.main != null){
                var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                var thisPosition = transform.position;
                direction = new Vector2(mousePosition.x - thisPosition.x, mousePosition.y - thisPosition.y);
                Destroy(gameObject,lifetime);
            }
        }

        private void FixedUpdate()
        {
            float x = transform.position.x + direction.normalized.x * speed * Time.fixedDeltaTime;
            float y = transform.position.y + direction.normalized.y * speed * Time.fixedDeltaTime;
            transform.position = new Vector2(x, y);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player")){
                Debug.Log("Player");
                return;
            }
            Destroy(gameObject);
            Debug.Log(other.name);
        }
    }
}