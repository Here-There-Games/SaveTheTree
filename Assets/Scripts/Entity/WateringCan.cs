using UnityEngine;

namespace Entity
{
    public class WateringCan : MonoBehaviour
    {
        [SerializeField] private float attackPreparing;
        [SerializeField] private float attackCooldown = 1;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform muzzle;

        private Attack attack;

        private void Start()
        {
            attack = new Attack(AttackBehaviour, attackPreparing, attackCooldown);
        }

        public void Attack()
        {
            if(attack.TryStart())
                Debug.Log("Attack Successful. Play Animation");
            else{
                Debug.Log("Attack Failed");
            }
        }

        private void AttackBehaviour()
        {
            var instance = Instantiate(bulletPrefab,muzzle);
        }
    }
}