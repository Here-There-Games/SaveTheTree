using System;
using System.Reflection;

namespace Core.Utilities.Editor
{
    public static class UnityEditorUtility
    {
        public static GetFieldTypeResult GetFieldType(FieldInfo fieldInfo)
        {
            bool isArray = fieldInfo.FieldType.IsArray;
            bool isList = fieldInfo.FieldType.IsGenericType;
            Type type = isArray ? fieldInfo.FieldType.GetElementType() :
                        isList ? fieldInfo.FieldType.GetGenericArguments()[0] :
                        fieldInfo.FieldType;
            return new GetFieldTypeResult(type, isArray, isList);
        }
        
        public readonly struct GetFieldTypeResult
        {
            public bool IsArray { get; }
            public bool IsList { get; }
            public Type FieldType { get; }

            public GetFieldTypeResult(Type fieldType, bool isArray, bool isList)
            {
                IsArray = isArray;
                IsList = isList;
                FieldType = fieldType;
            }
        }
    }
}