using Core.Utilities.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(PickerDataBaseAttribute))]
    public class PickerDataBaseDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            if(typeof(MonoBehaviour).IsAssignableFrom(fieldInfo.FieldType)){
                EditorGUI.HelpBox(position, $"Field {fieldInfo.Name} is MonoBehaviour", MessageType.Error);
                return;
            }
            
            var getFieldResult = UnityEditorUtility.GetFieldType(fieldInfo);

            Type arrayType = getFieldResult.FieldType!.MakeArrayType();
            Type listType = typeof(List<>).MakeGenericType(getFieldResult.FieldType);

            if(getFieldResult.IsList && !getFieldResult.FieldType.IsInterface || getFieldResult.IsArray && !getFieldResult.FieldType.IsInterface || !getFieldResult.FieldType!.IsInterface){
                EditorGUI.HelpBox(position, $"Field {fieldInfo.Name} is not interface", MessageType.Error);
                return;
            }

            PickerDataBaseAttribute dataBaseAttribute = attribute as PickerDataBaseAttribute;
            bool canDraw = property.managedReferenceValue == null;
            Rect buttonRect = EditorGUI.PrefixLabel(position, label);

            if(canDraw){
                if(!GUI.Button(buttonRect, $"Select {getFieldResult.FieldType.Name}")){
                    return;
                }

                string[] guids = AssetDatabase.FindAssets($"t:{dataBaseAttribute!.TypeDataBase.Name}");

                if(guids.Length == 0){
                    Debug.LogWarning("Count DataBased is 0");
                    return;
                }

                GenericMenu dataBaseMenu = new GenericMenu();

                foreach(string guid in guids){
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    Object dataBase = AssetDatabase.LoadAssetAtPath(path, dataBaseAttribute.TypeDataBase);
                    string currentName = dataBase.name;
                    Dictionary<GUIContent, object> elementDictionary = new Dictionary<GUIContent, object>();
                    MemberInfo[] members = dataBase.GetType().GetMembers();

                    foreach(MemberInfo memberInfo in members){
                        object element = memberInfo switch{
                            FieldInfo field => field.GetValue(dataBase),
                            PropertyInfo propertyInfo => propertyInfo.GetValue(dataBase),
                            _ => null
                        };
                        if(element == null || string.IsNullOrEmpty(element.ToString()))
                            continue;
                        Type elementType = element.GetType();
                        GUIContent elementPath = new GUIContent(currentName + "/" + element);

                        if(getFieldResult.FieldType.IsAssignableFrom(elementType))
                            elementDictionary.Add(elementPath, element);

                        if((elementType == arrayType || elementType == listType) && element is IEnumerable array){
                            foreach(object elementInArray in array){
                                if(elementInArray == null || string.IsNullOrEmpty(elementInArray.ToString())){
                                    continue;
                                }

                                elementDictionary.Add(new GUIContent($"{currentName} + /{elementInArray} \t({elementInArray.GetType().Name})"),
                                                      elementInArray);
                            }
                        }
                    }

                    elementDictionary = elementDictionary.OrderBy(pair => pair.Value.GetType().Name)
                                                         .ThenBy(pair => pair.Key.text)
                                                         .ToDictionary(pair => pair.Key, pair => pair.Value);

                    foreach(KeyValuePair<GUIContent, object> pair in elementDictionary)
                        dataBaseMenu.AddItem(pair.Key, false, OnSelectedItem, pair.Value);
                }

                if(dataBaseMenu.GetItemCount() == 0)
                    dataBaseMenu.AddDisabledItem(new GUIContent("Empty"));
                dataBaseMenu.DropDown(buttonRect);
            }
            else{
                float buttonSize = EditorGUIUtility.singleLineHeight;
                Rect deleteButtonRect =
                    new Rect(position.width, position.y, buttonSize, buttonSize);
                EditorGUI.BeginProperty(position, label, property);
                object element = property.managedReferenceValue;
                EditorGUI.LabelField(buttonRect, element + $" \t({element!.GetType().Name})");
                if(GUI.Button(deleteButtonRect, "X"))
                    property.managedReferenceValue = null;
                EditorGUI.EndProperty();
            }

            return;

            void OnSelectedItem(object userdata)
            {
                property.serializedObject.Update();

                property.managedReferenceValue = userdata;

                property.isExpanded = false;
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if(property.isExpanded)
                return base.GetPropertyHeight(property, label) * property.CountInProperty();
            return base.GetPropertyHeight(property, label);
        }
    }
}