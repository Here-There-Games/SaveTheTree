using System;
using Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackCooldown;

    private new Rigidbody2D rigidbody;
    private Transform enemyTransform;
    private Transform treeTransform;
    private Tree tree;
    private Timer timer;
    private IControllable controllable;

    private void Awake()
    {
        controllable = GetComponent<IControllable>();
        rigidbody = GetComponent<Rigidbody2D>();

        tree = FindObjectOfType<Tree>();
        treeTransform = tree.transform;
        enemyTransform = transform;

        timer = new Timer(this, attackCooldown);
        timer.OnStart += Attack;
        if(controllable == null)
            throw new NullReferenceException($"Add to {name} a class who realise IControllable interface");
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(rigidbody.position, treeTransform.position) <= attackRadius){
            controllable.Move(Vector2.zero);
            if(timer.Stopped)
                timer.Start();
        }
        else{
            Vector2 direction = treeTransform.position - enemyTransform.position;
            controllable.Move(direction);
        }
    }

    private void Attack()
    {
        print("Attack");
    }
}