﻿using MalbersAnimations.Scriptables;
using UnityEngine;

namespace MalbersAnimations.Controller.AI
{
    [CreateAssetMenu(menuName = "Malbers Animations/Pluggable AI/Tasks/Set Target")]
    public class SetTargetTask : MTask
    {
        public enum TargetToFollow { Transform, GameObject, RuntimeGameObjects }

        [Space]
        public TargetToFollow targetType = TargetToFollow.Transform;

        [RequiredField]  public TransformVar TargetT;
        [RequiredField] public GameObjectVar TargetG;
        [RequiredField] public RuntimeGameObjects TargetRG;
        public GetRuntimeGameObjects.RuntimeSetTypeGameObject rtype = GetRuntimeGameObjects.RuntimeSetTypeGameObject.Random;

        public IntReference RTIndex = new IntReference();
        public StringReference RTName = new StringReference();

        public override void StartTask(MAnimalBrain brain, int index)
        {
            switch (targetType)
            {
                case TargetToFollow.Transform:
                    brain.AIMovement.SetTarget(TargetT.Value);
                    break;
                case TargetToFollow.GameObject:
                    brain.AIMovement.SetTarget(TargetG.Value);
                    break;
                case TargetToFollow.RuntimeGameObjects:
                    switch (rtype)
                    {
                        case GetRuntimeGameObjects.RuntimeSetTypeGameObject.First:
                            brain.AIMovement.SetTarget(TargetRG.Item_GetFirst());
                            break;
                        case GetRuntimeGameObjects.RuntimeSetTypeGameObject.Random:
                            brain.AIMovement.SetTarget(TargetRG.Item_GetRandom());
                            break;
                        case GetRuntimeGameObjects.RuntimeSetTypeGameObject.Index:
                            brain.AIMovement.SetTarget(TargetRG.Item_Get(RTIndex));
                            break;
                        case GetRuntimeGameObjects.RuntimeSetTypeGameObject.ByName:
                            brain.AIMovement.SetTarget(TargetRG.Item_Get(RTName));
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            brain.TaskDone(index);
        }



        void Reset() { Description = "Set a new Target to the AI Animal Control, it uses Run time sets Transforms or GameObjects"; }


    }


#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(SetTargetTask))]
    public class SetTargetTaskEditor : UnityEditor.Editor
    {
        UnityEditor.SerializedProperty Description, MessageID, targetType, TargetT, TargetG, TargetRG, rtype, RTIndex, RTName;

        private void OnEnable()
        {
            Description = serializedObject.FindProperty("Description");
            MessageID = serializedObject.FindProperty("MessageID");
            targetType = serializedObject.FindProperty("targetType");
            TargetT = serializedObject.FindProperty("TargetT");
            TargetG = serializedObject.FindProperty("TargetG");
            TargetRG = serializedObject.FindProperty("TargetRG");
            rtype = serializedObject.FindProperty("rtype");
            RTIndex = serializedObject.FindProperty("RTIndex");
            RTName = serializedObject.FindProperty("RTName");
            
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            UnityEditor.EditorGUILayout.PropertyField(Description);
            UnityEditor.EditorGUILayout.PropertyField(MessageID);
            UnityEditor.EditorGUILayout.Space();
            UnityEditor.EditorGUILayout.HelpBox("All targets must be set at Runtime. Scriptable asset cannot have scenes References", UnityEditor.MessageType.Info);
          
            UnityEditor.EditorGUILayout.PropertyField(targetType);

            var tt = (SetTargetTask.TargetToFollow) targetType.intValue;

            switch (tt)
            {
                case SetTargetTask.TargetToFollow.Transform:
                    UnityEditor.EditorGUILayout.PropertyField(TargetT, new GUIContent("Target"));
                    break;
                case SetTargetTask.TargetToFollow.GameObject:
                    UnityEditor.EditorGUILayout.PropertyField(TargetG, new GUIContent("Target"));
                    break;
                case SetTargetTask.TargetToFollow.RuntimeGameObjects:
                    UnityEditor.EditorGUILayout.PropertyField(TargetRG, new GUIContent("Target"));
                    UnityEditor.EditorGUILayout.PropertyField(rtype, new GUIContent("Selection"));

                    var Sel = (GetRuntimeGameObjects.RuntimeSetTypeGameObject)rtype.intValue;
                    switch (Sel)
                    {
                        case GetRuntimeGameObjects.RuntimeSetTypeGameObject.Index:
                            UnityEditor.EditorGUILayout.PropertyField(RTIndex, new GUIContent("Element Index"));
                            break;
                        case GetRuntimeGameObjects.RuntimeSetTypeGameObject.ByName:
                            UnityEditor.EditorGUILayout.PropertyField(RTName, new GUIContent("Element Name"));
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
