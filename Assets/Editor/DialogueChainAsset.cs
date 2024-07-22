using UnityEngine;
using UnityEditor;
using System.IO;

public class DialogueChainAsset
{
    [MenuItem("Assets/Create/DialogueChain")]
    public static void CreateAsset()
    {
        CustomAssetUtility.CreateAsset<DialogueChain>();
    }
}
