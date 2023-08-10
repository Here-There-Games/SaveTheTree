using Interfaces;
using UnityEngine;

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
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void Shoot(Vector2 direction)
        {
            Collider2D hit = Physics2D.OverlapCircle(attack.Point.position, attack.Range, attack.Layer);

            if(hit != null)
                attack.TryAttack(hit);
            else
                Debug.Log("Hit is not touched");
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