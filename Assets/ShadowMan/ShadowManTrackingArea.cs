using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManTrackingArea : MonoBehaviour
{
    public delegate void OnPlayerAreaChange(bool enter);
    public OnPlayerAreaChange onPlayerAreaChange;

    private void OnTriggerEnter(Collider other)
    {
        if (CheckIfPlayer(other))
        {
            onPlayerAreaChange?.Invoke(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (CheckIfPlayer(other))
        {
            onPlayerAreaChange?.Invoke(false);
        }
    }
    bool CheckIfPlayer(Collider collider)
    {
        return collider.TryGetComponent(out PlayerController pc);
    }
}
