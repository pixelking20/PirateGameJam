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
    public Animator animator;

    [Tooltip("Floor Layer")]
    [SerializeField] LayerMask floorMask;

    [Tooltip("Interaction Layer")]
    [SerializeField] LayerMask interactionMask;

    Vector3 input;

    CharacterController controller;

    Vector3 startingPos;
    Quaternion startingRot;
    Collider myCol;
    [SerializeField]
    DialogueChain onRespawnDialogue;
    bool controlsLocked;
    private void Awake()
    {
        startingPos = transform.position;
        startingRot = transform.rotation;
        myCol = transform.GetComponent<Collider>();
    }

    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!controlsLocked)
        {
            GetInput();

            if(input != Vector3.zero) {
                animator.SetBool("Walking", true);
                animator.speed = 1.5f;
            } else {
                animator.SetBool("Walking", false);
                animator.speed = 1f;
            }

            transform.Rotate(new Vector3(0, input.x * rotationSpeed * Time.deltaTime, 0));
            controller.Move(transform.forward * input.z * moveSpeed * Time.deltaTime);

            bool interactPressed = InputHandler.GetInput(Inputs.Interact, ButtonInfo.Press); //Can Check if any button was pressed, released, or held this frame.

            if (interactPressed)
            {
                CheckForAvailableInteraction();
            }
        }
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
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f, floorMask)) {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }

    IEnumerator Interact() {
        controlsLocked = true;
        animator.speed = 1f;
        animator.SetTrigger("Interact");
        yield return new WaitForSeconds(0.5f);
        controlsLocked = false;
    }

    void CheckForAvailableInteraction() {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward, Color.green);
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1.5f, interactionMask)) {
            hit.transform.gameObject.SendMessage("Interact");
            StartCoroutine(Interact());
        }
    }

    IEnumerator Respawn()
    {
        controlsLocked = true;
        myCol.enabled = false;
        yield return TransitionCurtains.Transition(false);
        transform.position = startingPos;
        transform.rotation = startingRot;
        myCol.enabled = true;
        yield return TransitionCurtains.Transition(true);
        yield return DialogueManager.HandleDialogueChain(onRespawnDialogue);
        controlsLocked = false;
    }

    public void LockControls()
    {
        Debug.Log("Controls locked");
        controlsLocked = true;
    }

    public void UnlockControls()
    {
        controlsLocked = false;
    }
    public void GetHitByShadowMonster()
    {
        StartCoroutine(Respawn());
    }
}
