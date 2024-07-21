using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace SceneBlocks
{

    [CustomEditor(typeof(SceneBlockManager))]
    public class SceneBlockManagerEditor : Editor
    {
        private SerializedProperty BlocksArray;
        private void OnEnable()
        {
            BlocksArray = serializedObject.FindProperty("Blocks");
        }

        public override void OnInspectorGUI()
        {
            var BlockNames = Enum.GetNames(typeof(SceneBlockEnum));

            BlocksArray.arraySize = BlockNames.Length;

            for (int i = 0; i < BlockNames.Length; i++)
            {
                EditorGUILayout.PropertyField(BlocksArray.GetArrayElementAtIndex(i), new GUIContent(BlockNames[i]));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
