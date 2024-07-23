using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : Interactable
{
    public bool active = false;
    public Light connectedLight;

    private void Start() {
        connectedLight.enabled = active;
    }

    public override void Interact()
    {
        connectedLight.enabled = !connectedLight.enabled;
    }
}
