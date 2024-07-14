using Core.Core.Utilities;
using UnityEngine;

namespace Entity
{
    public class WateringCan : MonoBehaviour
    {
        [SerializeField] private float attackPreparing;
        [SerializeField] private float attackCooldown = 1;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform muzzle;
        [SerializeField] private float attackRange;

        private Attack attack;
        private Vector2 attackDirection = Vector2.zero;

        private void Start()
        {
            attack = new Attack(AttackBehaviour, attackPreparing, attackCooldown);
        }

        public void Attack(Vector2 direction)
        {
            if(attack.TryStart()){
                attackDirection = direction;
                return;
            }

            Debug.Log("Attack Failed");
        }

        private void AttackBehaviour()
        {
            Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.identity)
                .With(b => b.Direction = attackDirection);
        }

        public void Rotate(Vector2 direction)
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

        private void Flip(bool flip)
        {
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(flip ? Mathf.Abs(scale.x) * -1 : Mathf.Abs(scale.x), scale.y, scale.z);
        }
    }
}