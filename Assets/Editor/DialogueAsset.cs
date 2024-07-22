using UnityEngine;
using UnityEditor;
using System.IO;

public class DialogueAsset
{
    [MenuItem("Assets/Create/Dialogue")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<Dialogue>();
    }
}
