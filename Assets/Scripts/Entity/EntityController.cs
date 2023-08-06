using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityController : MonoBehaviour, IControllable
{
    [SerializeField] private float speed;
    
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if(rigidbody.gravityScale != 0)
            rigidbody.gravityScale = 0;
    }

    public void Move(Vector2 direction)
    {
        rigidbody.velocity = direction.normalized * speed;
    }
}