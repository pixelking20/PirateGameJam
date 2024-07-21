using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;
using System;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 2.5f;
    public float rotationSpeed = 200f;

    [Tooltip("Floor Layer")]
    [SerializeField] LayerMask floorMask;

    Vector3 input;

    CharacterController controller;

    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        transform.Rotate(new Vector3(0, input.x * rotationSpeed * Time.deltaTime, 0));
        controller.Move(transform.forward * input.z * moveSpeed * Time.deltaTime);
        CheckForAvailableInteraction();
    }

    void LateUpdate() {
        SnapToGround();
    }

    Vector3 GetInput() {
        float forward = InputHandler.GetYAxis();
        float side = InputHandler.GetXAxis();

        input = new Vector3(side,0,forward);

        return input.normalized;
    }

    void SnapToGround() {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f)) {
            print(hit.point);
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    void CheckForAvailableInteraction() {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.green);
        //either ray or spherecast, i'm just putting this here as the placeholder of where interaction code would be placed.
    }
}
