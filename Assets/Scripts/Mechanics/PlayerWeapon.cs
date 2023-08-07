using System;
using Common;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mechanics
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private EntityAttack attack;

        private InputAction shoot;
        private new Camera camera;
        private Vector2 direction;

        private void Awake()
        {
            camera = Camera.main;
            attack.Init(this);
        }

        private void Start()
        {
            shoot = InputManager.Instance.InputControl.Player.Fire;
            shoot.performed += ShootCallback; //c => Shoot();
        }

        private void ShootCallback(InputAction.CallbackContext obj)
        {
            if(obj.performed)
                Shoot();
        }

        private void Update()
        {
            direction = CalculateDirection();
        }

        private void FixedUpdate()
        {
            RotateWeapon();
        }

        public void RotateWeapon()
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private Vector2 CalculateDirection()
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            return mousePos - (Vector2)transform.position;
        }

        private void Shoot()
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