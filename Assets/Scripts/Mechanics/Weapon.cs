using Interfaces;
using UnityEngine;

namespace Mechanics
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [field: SerializeField] public bool CanRotate { get; private set; }

        [SerializeField] private EntityAttackRange attack;
        [SerializeField] private float attackAmplitude;
        [SerializeField] private float attackFrequency;

        private CameraShake cameraShake;

        private void Awake()
        {
            cameraShake = FindObjectOfType<CameraShake>();
            cameraShake.Init(attackAmplitude, attackFrequency);
            
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

        public void Attack(Vector2 targetDirection)
        {
            if(attack.TryAttack(targetDirection)){
                cameraShake.StartShake();
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