using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace SceneBlocks
{
    [CreateAssetMenu(fileName = "SceneBlock", menuName = "SceneBlocks/NewBlockObject", order = 1)]
    public class SceneBlock : ScriptableObject
    {
        [SerializeField]
        public string[] Block;
    }
}
