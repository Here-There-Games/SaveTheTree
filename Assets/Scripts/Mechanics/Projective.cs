using Common.Utilities;
using Interfaces;
using UnityEngine;

namespace Mechanics
{
    public class Projective : MonoBehaviour
    {
        [SerializeField] private float speed;

        private bool initialized;
        private new Rigidbody2D rigidbody;
        private EntityAttackRange entityAttack;
        private Transform thisTransform;
        private Vector2 direction;
        private Vector3 startPoint;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            thisTransform = transform;
        }

        private void FixedUpdate()
        {
            if(!initialized)
                return;
            rigidbody.velocity = direction * speed;
            if(Vector3.Distance(startPoint, thisTransform.position) > entityAttack.Range)
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.CheckTouchLayer(entityAttack.Layer)){
                col.GetComponent<ITakeDamage>().TakeDamage(entityAttack);
                Destroy(gameObject);
            }
        }

        public void InitBullet(Vector2 dir, EntityAttackRange attack)
        {
            direction = dir;
            entityAttack = attack;
            initialized = true;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(new Vector3(0,0, angle));
            startPoint = thisTransform.position;
        }
    }
}