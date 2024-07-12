﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace Entity
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damage = 1;
        [SerializeField] private float speed = 10;
        [SerializeField] private float lifetime = 5;
        public Vector2 Direction;

        private void Awake()
        {
            Destroy(gameObject, lifetime);
        }

        private void FixedUpdate()
        {
            float x = transform.position.x + Direction.normalized.x * speed * Time.fixedDeltaTime;
            float y = transform.position.y + Direction.normalized.y * speed * Time.fixedDeltaTime;
            transform.position = new Vector2(x, y);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player")){
                Debug.Log("Player");
                return;
            }

            Debug.Log(other.name);
            Destroy(gameObject);
        }
    }
}