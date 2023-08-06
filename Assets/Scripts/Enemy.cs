using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackCooldown;
    
    private new Rigidbody2D rigidbody;
    // private Player player;
    private Tree tree;
    private Transform enemyTransform;
    private Transform treeTransform;
    private Timer timer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        tree = FindObjectOfType<Tree>();
        treeTransform = tree.transform;
        enemyTransform = transform;
        
        // player = FindObjectOfType<Player>();

        timer = new Timer(this, attackCooldown);
        timer.OnStart += Attack;

        if(rigidbody.gravityScale != 0)
            rigidbody.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(rigidbody.position, treeTransform.position) <= attackRadius){
            rigidbody.velocity = Vector2.zero;
            if(timer.Stopped)
                timer.Start();
        }
        else{
            Vector2 direction = treeTransform.position - enemyTransform.position;
            // transform.Translate(direction * speed * Time.fixedDeltaTime);
            rigidbody.velocity = direction.normalized * speed;
        }
    }

    private void Attack()
    {
        print("Attack");
    }
}