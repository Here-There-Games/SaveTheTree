using Interfaces;
using UnityEngine;

namespace Mechanics
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private EntityAttack attack;
        [field: SerializeField] public bool CanRotate { get; private set; }
        [field: SerializeField] public bool CanAttack { get; private set; }

        public void RotateWeapon(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void Shoot(Vector2 direction)
        {
            
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col != null && !col.GetComponent<Tree>() && attack.TryAttack())
                col.GetComponent<ITakeDamage>().TakeDamage(attack);
        }
    }
}