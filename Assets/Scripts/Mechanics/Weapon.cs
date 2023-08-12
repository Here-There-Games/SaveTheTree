using System;
using Entity;
using Interfaces;
using UnityEngine;
using Tree = Entity.Tree;

namespace Mechanics
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private EntityAttackRange attack;
        [field: SerializeField] public bool CanRotate { get; private set; }

        private void Awake()
        {
            attack.Init(this);
        }

        public void RotateWeapon(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if(angle is > 90 or < -90){
                Flip(true);
                transform.rotation = Quaternion.Euler(0, 0, angle - 180);
            }
            else{
                Flip(false);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        public void Attack()
        {
            Collider2D hit = Physics2D.OverlapCircle(attack.Point.position, attack.Range, attack.Layer);

            if(hit != null && attack.TryAttack())
                hit.GetComponent<ITakeDamage>()?.TakeDamage(attack);
            else
                Debug.Log("Hit is not touching");
        }
        
        private void Flip(bool flip)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(flip ? Mathf.Abs(scale.x) * -1 : Mathf.Abs(scale.x), scale.y, scale.z);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attack.Point.position,attack.Range);
        }
#endif
        
    }
}