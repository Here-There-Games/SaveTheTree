using Interfaces;
using UnityEngine;

namespace Mechanics
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private EntityAttack attack;
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
            if(attack.TryAttack()){
                RaycastHit2D hit2D = 
                    Physics2D.Raycast(transform.position, direction, attack.Range, attack.Layer);

                if(hit2D.collider == null || !hit2D.transform){
                    Debug.Log("Hit is not touching");
                    return;
                }
                hit2D.collider.GetComponent<ITakeDamage>()?.TakeDamage(attack);
            }
        }
    }
}