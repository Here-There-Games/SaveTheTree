using System;
using Entity;
using Interfaces;
using UnityEngine;
using Tree = UnityEngine.Tree;

namespace Mechanics
{
    public class Projective : MonoBehaviour
    {
        [SerializeField] private float speed;

        private new Rigidbody2D rigidbody;
        private LayerMask mask;
        private IDamage iDamage;
        private Vector2 direction;
        private bool initialized;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if(!initialized)
                return;
            rigidbody.velocity = direction * speed;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if((mask.value & (1 << col.gameObject.layer)) != 0){
                col.GetComponent<ITakeDamage>().TakeDamage(iDamage);
                Destroy(gameObject);
            }
        }

        /*
        private void OnCollisionEnter2D(Collision2D col)
        {
            print(col.collider.name + " " + (col.gameObject.layer == mask));
            if((mask.value & (1 << col.gameObject.layer)) != 0)
                print(col.gameObject.name + " is touched");
            // if(col.collider.GetComponent<Tree>() || col.collider.GetComponent<Player>()){
                // col.collider.GetComponent<ITakeDamage>().TakeDamage(iDamage);
                // Destroy(gameObject);
            // }
        }
        */

        public void InitBullet(Vector2 dir, IDamage damage, LayerMask maskToAttack)
        {
            direction = dir;
            iDamage = damage;
            mask = maskToAttack;
            initialized = true;
            Destroy(gameObject, 5);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(new Vector3(0,0, angle));
        }

        public void LookTarget(Transform target)
        {
            transform.LookAt(target);
        }
    }
}