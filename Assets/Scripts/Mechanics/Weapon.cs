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
            if(attack.Can){
                RaycastHit2D hit =
                    Physics2D.Raycast(transform.position, direction, attack.Range, attack.Layer);

                if(hit.collider != null && attack.TryAttack(hit.collider)){
                    Debug.Log($"Hit {hit}");
                    Debug.Log($"Hit {hit.collider}");
                    Debug.Log($"Hit {hit.rigidbody}");
                    Debug.Log($"Hit {hit.distance}");
                    // hit2D.transform.gameObject.GetComponent<ITakeDamage>()?.TakeDamage(attack);
                    // hit2D.collider.GetComponent<ITakeDamage>()?.TakeDamage(attack);
                    Debug.Log($"Hit {hit.collider.name} and {hit.transform.name}");
                }
                else{
                    Debug.Log($"Hit is not touches");
                }
            }
        }
    }
}