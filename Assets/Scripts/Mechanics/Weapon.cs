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

            if(angle is > 90 or < -90){
                Flip(true);
                // angle = Mathf.Clamp(angle, -90, 90);
                transform.rotation = Quaternion.Euler(0, 0, angle - 180);
            }
            else{
                Flip(false);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            // angle = Mathf.Clamp(angle, -90, 90);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col != null && !col.GetComponent<Tree>() && attack.TryAttack())
                col.GetComponent<ITakeDamage>().TakeDamage(attack);
        }

        private void Flip(bool flip)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(flip ? Mathf.Abs(scale.x) * -1 : Mathf.Abs(scale.x), scale.y, scale.z);
        }
    }
}