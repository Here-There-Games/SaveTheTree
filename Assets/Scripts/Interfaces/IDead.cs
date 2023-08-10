using UnityEngine.Events;

namespace Interfaces
{
    public interface IDead
    {
        event UnityAction DiedEvent;
        void Dead();
    }
}