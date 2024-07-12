using System;
using UnityEngine;

namespace Core.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class PickerDataBaseAttribute : PropertyAttribute
    {
        public Type TypeDataBase { get; }

        public PickerDataBaseAttribute(Type typeDataBase)
        {
            TypeDataBase = typeDataBase;
        }
    }
}