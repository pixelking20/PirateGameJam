using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    protected Minigame minigame;
    protected Camera gameCam;
    Vector3 startingPosition;
    protected bool pickupable = false,pickedUp;

    public delegate void OnPickup();
    public OnPickup onPickup;

    private void Awake()
    {
        minigame = GetComponentInParent<Minigame>();
        gameCam = minigame.GetCamera();
        startingPosition = transform.localPosition;
        OnAwake();
    }
    protected abstract void OnAwake();
    protected void Pickup()
    {
        if (pickupable && !pickedUp)
        {
            onPickup?.Invoke();
            pickedUp = true;
            OnToolPickup();
        }
    }
    protected abstract void OnToolPickup();
    public void Drop()
    {
        if (pickedUp)
        {
            pickedUp = false;
            OnDrop();
        }
    }
    protected abstract void OnDrop();
    public void ResetTool()
    {
        transform.localPosition = startingPosition;
        OnResetTool();
        Drop();
    }
    protected abstract void OnResetTool();
    public void SetPickupable(bool state)
    {
        pickupable = state;
    }
}
