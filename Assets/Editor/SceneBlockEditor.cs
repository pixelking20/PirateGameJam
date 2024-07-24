using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SceneBlocks;
using CustomUIEvents;
using System;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(SceneBlock))]
public class SceneBlockEditor : Editor
{
    SerializedProperty blockArray;
    private void OnEnable()
    {
        blockArray = serializedObject.FindProperty("Block");
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label($"Block Array", EditorStyles.boldLabel);

        for (int i = 0; i < blockArray.arraySize; i++)
        {

            if (EditorGUILayout.DropdownButton(new GUIContent(blockArray.GetArrayElementAtIndex(i).stringValue), FocusType.Passive))
            {
                GenericMenu dropDown = new GenericMenu();

                for (int y = 0; y < EditorBuildSettings.scenes.Length; y++)
                {
                    string sceneName = EditorBuildSettings.scenes[y].path;
                    dropDown.AddItem(new GUIContent(sceneName),  sceneName == blockArray.GetArrayElementAtIndex(i).stringValue, AssignSceneName, new int[2] {i,y});
                }

                dropDown.ShowAsContext();
            }
        }

        if (GUILayout.Button("Add Scene"))
        {
            blockArray.arraySize++;
        }

        if (GUILayout.Button("Remove Last Scene"))
        {
            blockArray.arraySize--;
        }


        serializedObject.ApplyModifiedProperties();
    }

    public void AssignSceneName(object indexs)
    {
        int[] indexArray = indexs as int[];

        blockArray.GetArrayElementAtIndex(indexArray[0]).stringValue = EditorBuildSettings.scenes[indexArray[1]].path;
    }
}
