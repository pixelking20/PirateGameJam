using SceneBlocks;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneBlocks
{
    public class SceneBlockManager : MonoBehaviour
    {
        
        public SceneBlock[] Blocks;

        private SceneBlockEnum currentSceneIndex;

        private bool ActivelySwapping;

        public static SceneBlockManager Instance
        {
            get;

            private set;
        }

        // Start is called before the first frame update
        void Start()
        {
            //if instance is already instantiated, we are the extra copy
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(this.gameObject);

            currentSceneIndex = 0;
        }

        /// <summary>
        /// Method to notify the Scene Manager to change scenes to the specified block
        /// </summary>
        /// <param name="setScene"></param>
        public void ChangeSceneBlock(SceneBlockEnum setScene)
        {
            //early return since we are already in that scene block
            if (currentSceneIndex == setScene)
            {
                return;
            }

            if (ActivelySwapping == true)
            {
                Debug.LogWarning("The scene manage is actively swapping scenes. The call to swap scenes from this call stick will be ignored");
                return;
            }

            ActivelySwapping = true;

            var coroutine = SceneChange(setScene);

            StartCoroutine(coroutine);
        }

        /// <summary>
        /// Coroutine to facilitate the loading process in unity. 
        /// Since Unity needs at least one scene loaded before it can start unloading any scene, this ensures that at least one scene is loaded by waiting on the first async load request.
        /// </summary>
        /// <param name="newSceneValue">The new set of scenes that are to be loaded.</param>
        /// <returns>An IEnumerator so that the start Coroutine can launch it</returns>
        private IEnumerator SceneChange(SceneBlockEnum newSceneValue)
        {
            var firstSceneTrigger = LoadSceneBlock(Blocks[(int)newSceneValue]);

            yield return firstSceneTrigger;

            UnloadSceneBlock(Blocks[(int)currentSceneIndex]);

            currentSceneIndex = newSceneValue;

            ActivelySwapping = false;
        }

        /// <summary>
        /// Helper function to unload scenes that are loaded from a block.
        /// </summary>
        /// <param name="sceneBlock">The scene block set to be unloaded</param>
        private void UnloadSceneBlock(SceneBlock sceneBlock)
        {
            for (int i = 0; i < sceneBlock.Block.Length; i++)
            {
                SceneManager.UnloadSceneAsync(sceneBlock.Block[i]);
            }
        }

        /// <summary>
        /// Helper function to load scenes from a Scene Block additively
        /// </summary>
        /// <param name="sceneBlock">The sceneBlock that is to be loaded</param>
        /// <returns>The first async load operation. Used by the coroutine to keep timing with unloading</returns>
        private AsyncOperation LoadSceneBlock(SceneBlock sceneBlock)
        {
            //grabs and returns the first scene set to load so that 
            var firstAsyncMethod = SceneManager.LoadSceneAsync(sceneBlock.Block[0], LoadSceneMode.Additive);

            for (int i = 1; i < sceneBlock.Block.Length; i++)
            {
                SceneManager.LoadSceneAsync(sceneBlock.Block[i], LoadSceneMode.Additive);
            }

            return firstAsyncMethod;
        }
    }
}
