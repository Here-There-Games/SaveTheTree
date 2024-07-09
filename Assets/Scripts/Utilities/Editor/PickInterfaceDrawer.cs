using Core.Utilities.Attributes;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Core.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(PickInterfaceAttribute))]
    public class PickInterfaceDrawer : PropertyDrawer
    {
        private Type fieldType;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * property.CountInProperty()
                   + EditorGUIUtility.standardVerticalSpacing * 5;
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            UnityEditorUtility.GetFieldTypeResult getFieldResult = UnityEditorUtility.GetFieldType(fieldInfo);
            fieldType = getFieldResult.FieldType;
            EditorGUI.BeginProperty(position, label, property);

            if(property?.managedReferenceValue == null){
                DrawButtonToAdd(position, property, label);
            }
            else{
                GUIContent newLabel = new GUIContent(label.text + $"({property.managedReferenceValue.GetType().Name})");
                float buttonWidth = EditorGUIUtility.singleLineHeight;
                Rect classRect = new Rect(position.x, position.y, position.width - buttonWidth, position.height);
                Rect buttonRect = new Rect(classRect.x + classRect.width, classRect.y, buttonWidth, buttonWidth);
                EditorGUI.PropertyField(classRect, property, newLabel, true);
                if(GUI.Button(buttonRect, "X"))
                    property.managedReferenceValue = null;
            }

            EditorGUI.EndProperty();
        }

        private void DrawButtonToAdd(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect buttonRect = EditorGUI.PrefixLabel(position, label);

            if(!GUI.Button(buttonRect, $"Add {property.displayName}"))
                return;
            GenericMenu menu = new GenericMenu();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach(Assembly assembly in assemblies){
                Type[] allTypes = assembly.GetTypes()
                                          .Where(type => !type.IsAbstract && fieldType.IsAssignableFrom(type))
                                          .ToArray();

                foreach(Type type in allTypes)
                    menu.AddItem(new GUIContent($"{type.Name}"), false, OnSelectedItem, Activator.CreateInstance(type));
            }

            if(menu.GetItemCount() == 0)
                menu.AddDisabledItem(new GUIContent("Empty", "It is empty item"));
            menu.ShowAsContext();

            return;

            void OnSelectedItem(object userData)
            {
                property.serializedObject.Update();
                property!.managedReferenceValue = userData;
                property.isExpanded = true;
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}