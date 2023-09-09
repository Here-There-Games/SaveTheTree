using System;
using UnityEngine;

namespace Common.Utilities
{
    public static class Extensions
    {
        public static void With<T>(this T self, Action<T> apply)
            => apply.Invoke(self);

        public static void With<T>(this T self, Action<T> apply, bool when)
        {
            if(when)
                With(self, apply);
        }

        public static void With<T>(this T self, Action<T> apply, Func<bool> when)
        {
            if(when())
                With(self, apply);
        }

        public static bool CheckTouchLayer(this Collider2D self, LayerMask layer) 
            => (layer.value & (1 << self.gameObject.layer)) != 0;
    }
}