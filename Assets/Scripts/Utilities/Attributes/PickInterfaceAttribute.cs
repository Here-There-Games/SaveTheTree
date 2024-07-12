using System;
using UnityEngine;

namespace Core.Utilities.Attributes
{
    /// <summary>
    /// <example> [SerializeReference, PickInterface] private ITest test</example>
    /// Show generic window with all scripts that is assignable from ITest
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class PickInterfaceAttribute : PropertyAttribute
    {

        public Type Type { get; }

        public PickInterfaceAttribute()
        {
            
        }

        public PickInterfaceAttribute(Type type)
        {
            Type = type;
        }
    }
}