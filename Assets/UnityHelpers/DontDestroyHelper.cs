using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{
    public class DontDestroyHelper : MonoBehaviour
    {
        //quick Component that sets the gameobject it's attached to to not be destroyed on load.
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
