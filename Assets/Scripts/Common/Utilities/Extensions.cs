using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Utilities
{
    public static class Extensions
    {
        public static void CheckForNullComponents(Object sender, Component[] components)
            => CheckForNull(sender, components);

        private static void CheckForNull(Object sender, IEnumerable<Object> objects)
        {
            foreach(Object o in objects){
                if(o == null)
                    throw new NullReferenceException($"In {sender.name}:{o.GetType().Name} is null");
            }
        }
    }
}