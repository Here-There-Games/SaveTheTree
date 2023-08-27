using UnityEngine.Events;

namespace Interfaces
{
    public interface IDead
    {
        event UnityAction OnDiedEvent;
        void Dead();
    }
}