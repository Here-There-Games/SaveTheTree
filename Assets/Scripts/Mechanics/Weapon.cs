using Common.Utilities;
using Interfaces;
using UnityEngine;

namespace Mechanics
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        private static readonly int attack1 = Animator.StringToHash("Attack");
        [field: SerializeField] public bool CanRotate { get; private set; }

        [SerializeField] private EntityAttackRange attack;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponentInParent<Animator>();

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

            if(attack.TryAttack()){
                animator.SetTrigger(attack1);
                if(hit != null && hit.CheckTouchLayer(attack.Layer)){
                    hit.GetComponent<ITakeDamage>()?.TakeDamage(attack);
                }
            }
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
            Gizmos.DrawWireSphere(attack.Point.position, attack.Range);
        }
#endif
    }
}