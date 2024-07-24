using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableArea : MonoBehaviour
{
    public delegate void MouseOverStateChange(bool state);
    public MouseOverStateChange mouseOverStateChange;

    private void OnMouseEnter()
    {
        mouseOverStateChange?.Invoke(true);
    }
    private void OnMouseExit()
    {
        mouseOverStateChange?.Invoke(false);
    }
}
