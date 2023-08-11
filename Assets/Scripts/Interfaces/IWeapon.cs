using UnityEngine;

namespace Interfaces
{
    public interface IWeapon
    {
        bool CanRotate { get; }
        void RotateWeapon(Vector2 direction);
    }
}