using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : Interactable
{
    bool open = false;

    public Animator animator;

    private void Update() {
        animator.SetBool("IsOpen" , open);
    }

    public override void Interact() {
        open = !open;
    }
}
