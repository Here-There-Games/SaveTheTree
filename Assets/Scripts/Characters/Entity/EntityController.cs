using Characters.Entity;
using Entity;
using Interfaces;
using UnityEngine;

namespace Mechanics
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EntityController : MonoBehaviour, IControllable
    {
        private const string SPEED_STAT_NAME = "Speed";

        [SerializeField] private float defaultSpeed = 1;

        private new Rigidbody2D rigidbody;
        private StatHandle statHandle;
        private Animator animator;
        private static readonly int vertical = Animator.StringToHash("Vertical");
        private static readonly int horizontal = Animator.StringToHash("Horizontal");
        private float speed;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            statHandle = GetComponent<StatHandle>();

            EntityStat stat = new EntityStat(SPEED_STAT_NAME, defaultSpeed);

            if(statHandle != null){
                if(statHandle.StatData.TryCreateStat(stat))
                    Debug.Log($"{name} was create a Speed Stat");

                speed = statHandle.StatData.FindStat(SPEED_STAT_NAME).Value;
                statHandle.StatData.FindStat(SPEED_STAT_NAME).OnChangeEvent +=
                    value => speed = value;
            }
            else{
                Debug.LogError($"{name} is not find Stat Handle.");
            }

            if(rigidbody.gravityScale != 0)
                rigidbody.gravityScale = 0;
        }

        public void Move(Vector2 direction)
        {
            direction = direction.normalized;
            rigidbody.velocity = direction * speed;
            animator.SetFloat(horizontal, direction.x);
            animator.SetFloat(vertical, direction.y);
        }
    }
}