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
            Collider2D[] hits = Physics2D.OverlapCircleAll(attack.Point.position, attack.Range, attack.Layer);

            if(attack.CanAttack && hits.Length > 0 && attack.TryAttack())
                foreach(Collider2D hit in hits){
                    if(hit != null && hit.GetComponent<EnemyAI>())
                        hit.GetComponent<ITakeDamage>().TakeDamage(attack);
                }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col != null && !col.GetComponent<Tree>()) /*&& attack.TryAttack())*/
                col.GetComponent<ITakeDamage>().TakeDamage(attack);
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