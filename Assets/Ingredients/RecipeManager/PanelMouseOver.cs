using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelMouseOver : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    Animator animator;
    bool mouseOver = false;
    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate() {
        animator.SetBool("MouseOver", mouseOver);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
