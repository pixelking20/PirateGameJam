using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneBlocks
{
    /// <summary>
    /// Enum to be extended for each game. Intended to be set game sections. 
    /// 
    /// If a section needs to have multiple scenes open, it would be a single sceneBlock here but the scene manager would handle the multiple scenes that are needed.
    /// </summary>
    public enum SceneBlockEnum
    {
        GameStart = 0,
        DayOne = 1,
        DayTwo = 2,
        DayThree = 3,
        DayFour = 4,
        GameOver = 5
    }
}
