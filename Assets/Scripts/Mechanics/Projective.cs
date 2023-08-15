using Common.Utilities;
using Interfaces;
using UnityEngine;

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
            if(col.CheckTouchLayer(mask)){
                col.GetComponent<ITakeDamage>().TakeDamage(iDamage);
                Destroy(gameObject);
            }
        }

        public void InitBullet(Vector2 dir, IDamage damage, LayerMask maskToAttack)
        {
            direction = dir;
            iDamage = damage;
            mask = maskToAttack;
            initialized = true;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(new Vector3(0,0, angle));
            Destroy(gameObject, 5);
        }
    }
}