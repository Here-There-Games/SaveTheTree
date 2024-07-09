using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utilities.Editor
{
    public static class EditorTools
    {
        private static Dictionary<Type, MonoScript> typeMonoScriptLookup;

        public static SerializedProperty FindAutoProperty(this SerializedObject serializedObject, string nameProperty)
            => serializedObject.FindProperty($"<{nameProperty}>k__BackingField");

        public static ReorderableList CreateReorderable(string nameList,
                                                        SerializedProperty propertyList,
                                                        SerializedObject serializedObject,
                                                        ReorderableList.AddCallbackDelegate
                                                            AddCallbackDelegate = null,
                                                        ReorderableList.ElementCallbackDelegate drawElementCallback =
                                                            null,
                                                        ReorderableList.RemoveCallbackDelegate removeCallback = null)

        {
            ReorderableList reorderList = new ReorderableList(serializedObject, propertyList){
                drawHeaderCallback = rect =>
                                         {
                                             GUIStyle style = new GUIStyle(GUI.skin.label)
                                                 { alignment = TextAnchor.MiddleCenter };
                                             EditorGUI.LabelField(rect, nameList, style);
                                         },
            };
            if(removeCallback != null)
                reorderList.onRemoveCallback = removeCallback;
            if(drawElementCallback != null)
                reorderList.drawElementCallback = drawElementCallback;
            if(AddCallbackDelegate != null)
                reorderList.onAddCallback = AddCallbackDelegate;
            
            return reorderList;
        }


        public static void DeleteAsset(int index, SerializedProperty list)
        {
            Object obj = list.GetArrayElementAtIndex(index).objectReferenceValue;
            list.DeleteArrayElementAtIndex(index);
            AssetDatabase.RemoveObjectFromAsset(obj);
            Object.DestroyImmediate(obj, true);
            AssetDatabase.SaveAssets();
            list.serializedObject.ApplyModifiedProperties();
        }

        public static T CreateWithChild<T>(Object targetObj,
                                                 SerializedProperty list,
                                                 SerializedObject serializedObject,
                                                 bool resizeArray = true) where T : ScriptableObject
        {
            T instance = ScriptableObject.CreateInstance<T>();
            
            instance.name = $"new {typeof(T).Name}";
            AssetDatabase.AddObjectToAsset(instance, targetObj);
            AssetDatabase.SaveAssets();

            if(resizeArray){
                list.arraySize++;
                list.GetArrayElementAtIndex(list.arraySize - 1).objectReferenceValue = instance;
            }
            serializedObject.ApplyModifiedProperties();
            return instance;
        }

        public static T Create<T>(bool displayFilePanel) where T : ScriptableObject
            => (T)Create(typeof(T), displayFilePanel);

        public static ScriptableObject Create(Type type, bool displayFilePanel)
        {
            if(displayFilePanel){
                string mPath = EditorUtility.SaveFilePanelInProject($"Create {type.Name} type",
                                                                    $"New {type.Name}.asset",
                                                                    "asset",
                                                                    $"You create a {type.Name}");
                return CreateAsset(type, mPath);
            }

            return CreateAsset(type);
        }

        public static ScriptableObject CreateAsset(Type type)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if(string.IsNullOrEmpty(path))
                path = "Assets";
            else if(System.IO.Path.GetExtension(path) != "")
                path = path.Replace(System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)),
                                    null);
            string assetsNameAndPath = AssetDatabase.GenerateUniqueAssetPath($"{path}/New {type.Name}.asset");
            return CreateAsset(type, assetsNameAndPath);
        }

        public static ScriptableObject CreateAsset(Type type, string path)
        {
            if(string.IsNullOrEmpty(path))
                return null;
            ScriptableObject data = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(data, path);
            AssetDatabase.SaveAssets();
            return data;
        }
    }
}