using System;

namespace Common.Utilities
{
    public static class Extensions
    {
        public static void With<T>(this T self, Action<T> apply) 
            => apply.Invoke(self);

        public static void With<T>(this T self, Action<T> apply, bool when)
        {
            if(when)
                With(self,apply);
        }
        public static void With<T>(this T self, Action<T> apply, Func<bool> when)
        {
            if(when())
                With(self,apply);
        }
    }
}