using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionCurtains : MonoBehaviour
{
    static TransitionCurtains instance;
    Image curtains;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        curtains = GetComponentInChildren<Image>();
    }
    public static IEnumerator Transition(bool curtainsOpen)
    {
        float transitionTime = .5f, timeElapsed = 0;
        Color imageColour = instance.curtains.color;
        while (timeElapsed < transitionTime)
        {
            timeElapsed += Time.deltaTime;

            instance.curtains.color = new Color(imageColour.r, imageColour.g, imageColour.b, Mathf.Lerp(curtainsOpen ? 1 : 0, curtainsOpen ? 0 : 1, timeElapsed / transitionTime));

            yield return new WaitForEndOfFrame();
        }
    }
}
