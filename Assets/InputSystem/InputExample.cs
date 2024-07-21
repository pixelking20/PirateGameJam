using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem; //Important to include the namespace

public class InputExample : MonoBehaviour
{
    void Update()
    {
        bool interactPressed = InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press); //Can Check if any button was pressed, released, or held this frame.

        if (interactPressed)
        {
            print("Interact was pressed this frame!");
        }

        bool interactReleased = InputHandler.GetInput(Inputs.Interact, ButtonInfo.Release);

        if (interactReleased)
        {
            print("Interact was released this frame!");
        }

        //print("XAxis is currently: " + InputHandler.GetXAxis()); //Can also check X or Y Axis directly for ease of use.
    }
}
