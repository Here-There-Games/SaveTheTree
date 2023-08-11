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
            if(col.GetComponent<Tree>() || col.GetComponent<Player>())
                col.GetComponent<ITakeDamage>().TakeDamage(iDamage);
        }

        public void InitBullet(Vector2 dir, IDamage damage, LayerMask maskToAttack)
        {
            direction = dir;
            iDamage = damage;
            mask = maskToAttack;
            initialized = true;
            Destroy(gameObject, 5);
            transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, dir.y, 0));
        }

        public void LookTarget(Transform target)
        {
            transform.LookAt(target);
        }
    }
}