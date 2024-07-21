using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
using System;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 2.5f;
    public float rotationSpeed = 0.5f;

    Vector3 inputState;

    CharacterController controller;

    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInputState();
        print(inputState);
        transform.Rotate(new Vector3(0, inputState.x * rotationSpeed, 0));
        controller.Move(transform.forward * inputState.z * moveSpeed * Time.deltaTime);
    }

    Vector3 GetInputState() {

        bool upHeld = InputHandler.GetInput(Inputs.Up, ButtonInfo.Held);
        bool downHeld = InputHandler.GetInput(Inputs.Down, ButtonInfo.Held);
        bool leftHeld = InputHandler.GetInput(Inputs.Left, ButtonInfo.Held);
        bool rightHeld = InputHandler.GetInput(Inputs.Right, ButtonInfo.Held);

        float forward = (upHeld ? 1 : 0) - (downHeld ? 1 : 0);
        float side = (rightHeld ? 1 : 0) - (leftHeld ? 1 : 0);

        inputState = new Vector3(side,0,forward);

        return inputState.normalized;
    }
}
