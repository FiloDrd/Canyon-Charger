﻿using UnityEngine;
 


namespace MalbersAnimations.Scriptables
{
    public class AddRuntimeGameObjects : MonoBehaviour
    {
        [CreateScriptableAsset] public RuntimeGameObjects Collection;

        private void OnEnable() => Collection?.Item_Add(gameObject);

        private void OnDisable() => Collection?.Item_Remove(gameObject);
    }


#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(AddRuntimeGameObjects)), UnityEditor.CanEditMultipleObjects]
    public class AddRuntimeGameObjectsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
          //  UnityEditor.EditorGUILayout.Space();
            UnityEditor.EditorGUILayout.PropertyField(serializedObject.FindProperty("Collection"));
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}