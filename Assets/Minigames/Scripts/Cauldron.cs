using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : PlayableArea
{
    [SerializeField]
    Color slow, fast, good;

    [SerializeField]
    MeshRenderer potionMesh;

    Coroutine ColorChangeCoroutine;
    public void SetPotionStatus(PotionStatus status)
    {
        switch (status)
        {
            case PotionStatus.Slow:
                StartColorChangecoroutine(slow);
                break;
            case PotionStatus.Good:
                StartColorChangecoroutine(good);
                break;
            case PotionStatus.Fast:
                StartColorChangecoroutine(fast);
                break;
        }
    }
    void StartColorChangecoroutine(Color colour)
    {
        if (!ReferenceEquals(ColorChangeCoroutine, null))
            StopCoroutine(ColorChangeCoroutine);
        ColorChangeCoroutine = StartCoroutine(ChangeColour(colour));
    }
    IEnumerator ChangeColour(Color goalColour)
    {
        float timeElapsed = 0, colourChangeTime = .5f;
        Color startColour = potionMesh.material.color;
        while(timeElapsed < colourChangeTime)
        {
            timeElapsed += Time.deltaTime;
            potionMesh.material.color = Color.Lerp(startColour, goalColour, timeElapsed / colourChangeTime);
            yield return new WaitForEndOfFrame();
        }
        ColorChangeCoroutine = null;
    }
}
