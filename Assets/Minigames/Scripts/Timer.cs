using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    Transform timerBar;
    private void Awake()
    {
        timerBar = transform.Find("TimerBar");
    }
    public void SetTimer(float totalTime,float timeLeft)
    {
        timerBar.localScale = new Vector3(timeLeft / totalTime, 1, 1);
    }
    public void Disable()
    {
        SetTimer(1, 0);
    }
}
