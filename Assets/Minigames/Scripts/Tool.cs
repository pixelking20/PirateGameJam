using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public delegate void OnClick();
    public OnClick onClick;
    Minigame minigame;
    Camera gameCam;
    float distanceFromCam;
    [SerializeField]
    PlayableArea playArea;

    Vector3 startingPosition;

    Coroutine FollowMouseCoroutine;
    bool mouseInArea = true;
    Collider myCol;
    bool pickupable = false;
    private void Awake()
    {
        myCol = GetComponent<Collider>();
        minigame = GetComponentInParent<Minigame>();
        gameCam = minigame.GetCamera();
        startingPosition = transform.localPosition;
        distanceFromCam = Vector3.Distance(transform.position, gameCam.transform.position);
        playArea.mouseOverStateChange += MouseInAreaStateChange;
    }
    private void OnMouseDown()
    {
        if (pickupable)
        {
            onClick.Invoke();
            Pickup();
        }
    }
    void Pickup()
    {
        myCol.enabled = false;
        FollowMouseCoroutine = StartCoroutine(FollowMouse());
    }
    public void Drop()
    {
        myCol.enabled = true;
        if(!ReferenceEquals(FollowMouseCoroutine,null))
            StopCoroutine(FollowMouseCoroutine);
    }
    IEnumerator FollowMouse()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if(mouseInArea)
                transform.position = gameCam.transform.position + gameCam.ScreenPointToRay(Input.mousePosition).direction * distanceFromCam;
        }
    }
    void MouseInAreaStateChange(bool state)
    {
        mouseInArea = state;
    }
    public void ResetTool()
    {
        Drop();
        transform.position = startingPosition;
    }
    public void SetPickupable(bool state)
    {
        pickupable = state;
    }
}
