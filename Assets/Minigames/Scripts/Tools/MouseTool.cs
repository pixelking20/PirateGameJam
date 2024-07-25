using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTool : Tool
{
    [SerializeField]
    PlayableArea playArea;
    bool mouseInArea = true;
    float distanceFromCam;
    Collider myCol;
    Coroutine FollowMouseCoroutine;

    protected override void OnAwake()
    {
        myCol = GetComponent<Collider>();
        playArea.mouseOverStateChange += MouseInAreaStateChange;
        distanceFromCam = Vector3.Distance(transform.position, gameCam.transform.position);
    }
    void MouseInAreaStateChange(bool state)
    {
        mouseInArea = state;
    }
    private void OnMouseDown()
    {
        if (pickupable)
        {
            Pickup();
        }
    }
    protected override void OnToolPickup()
    {
        myCol.enabled = false;
        FollowMouseCoroutine = StartCoroutine(FollowMouse());
    }
    IEnumerator FollowMouse()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (mouseInArea)
                transform.position = gameCam.transform.position + gameCam.ScreenPointToRay(Input.mousePosition).direction * distanceFromCam;
        }
    }
    protected override void OnDrop()
    {
        myCol.enabled = true;
        if (!ReferenceEquals(FollowMouseCoroutine, null))
            StopCoroutine(FollowMouseCoroutine);
    }
    protected override void OnResetTool()
    {

    }
}
