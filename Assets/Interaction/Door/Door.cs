using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : Interactable
{
    bool open = false;

    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool("IsOpen" , open);
    }

    public override void Interact() {
        open = !open;
    }
}
