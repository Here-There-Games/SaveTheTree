using Interfaces;
using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EntityController : MonoBehaviour, IControllable
    {
        [SerializeField] private float speed;
    
        private Rigidbody2D rigidbody;
        private Animator animator;
        private static readonly int vertical = Animator.StringToHash("Vertical");
        private static readonly int horizontal = Animator.StringToHash("Horizontal");

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            if(rigidbody.gravityScale != 0)
                rigidbody.gravityScale = 0;
        }

        public void Move(Vector2 direction)
        {
            direction = direction.normalized;
            rigidbody.velocity = direction * speed;
            animator.SetFloat(horizontal, direction.x);
            animator.SetFloat(vertical, direction.y);
        }
    }
}