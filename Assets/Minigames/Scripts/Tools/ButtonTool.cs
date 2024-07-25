using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class ButtonTool : Tool
{
    private void Update()
    {
        if (minigame.GetMinigameRunning() && InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press))
        {
            Pickup();
        }
    }
    protected override void OnDrop()
    {
        
    }

    protected override void OnAwake()
    {
        
    }

    protected override void OnResetTool()
    {
        
    }

    protected override void OnToolPickup()
    {
        
    }
}
