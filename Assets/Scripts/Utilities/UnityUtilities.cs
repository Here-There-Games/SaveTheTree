using System;
using UnityEngine.InputSystem;

namespace Core.Core.Utilities
{
    public static class UnityUtilities
    {
        public static T With<T>(this T self, Action<T> action)
        {
            action.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> action, bool when)
        {
            if(when)
                action.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> action, Func<bool> when)
        {
            if(when())
                action.Invoke(self);
            return self;
        }

        public static string GetActionName(this InputActionReference actionReference)
        {
            int binding = actionReference.action.GetBindingIndex();
            return actionReference.action.GetBindingDisplayString(binding);
        }
    }
}