using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem

{
    public class InputHandler : MonoBehaviour
    {
        static InputHandler inputHandler;
        Button up = new Button(),
            down = new Button(),
            left = new Button(),
            right = new Button(),
            interact = new Button();

        private void Awake()
        {
            if (inputHandler)
            {
                Debug.LogError("More than one input handler in scene, not allowed >.<");
                Destroy(inputHandler);
            }

            inputHandler = this;
            transform.parent = FindObjectOfType<PlayerInputManager>().transform;
        }
        private void Update()
        {
            up.UpdateValues();
            down.UpdateValues();
            right.UpdateValues();
            left.UpdateValues();
            interact.UpdateValues();
        }
        public static bool GetInput(Inputs input, ButtonInfo info)
        {
            if (!inputHandler)
            {
                return false;
            }
            return inputHandler.GetInputLocal(input, info);
        }
        public static float GetYAxis()
        {
            return (GetInput(Inputs.Up, ButtonInfo.Held) ? 1 : 0) + (GetInput(Inputs.Down, ButtonInfo.Held) ? -1 : 0);
        }
        public static float GetXAxis()
        {
            return (GetInput(Inputs.Right, ButtonInfo.Held) ? 1 : 0) + (GetInput(Inputs.Left, ButtonInfo.Held) ? -1 : 0);
        }
        bool GetInputLocal(Inputs input, ButtonInfo info)
        {
            Button button = up;

            switch (input)
            {
                case Inputs.Up:
                    button = up;
                    break;
                case Inputs.Down:
                    button = down;
                    break;
                case Inputs.Left:
                    button = left;
                    break;
                case Inputs.Right:
                    button = right;
                    break;
                case Inputs.Interact:
                    button = interact;
                    break;
            }

            switch (info)
            {
                case ButtonInfo.Held:
                    return button.held;
                case ButtonInfo.Press:
                    return button.pressFrame;
                case ButtonInfo.Release:
                    return button.releaseFrame;
            }
            Debug.LogError("Should not be able to reach this point");
            return false;
        }
        public void OnUp(InputAction.CallbackContext context)
        {
            up.value = context.ReadValue<float>();
        }
        public void OnDown(InputAction.CallbackContext context)
        {
            down.value = context.ReadValue<float>();
        }
        public void OnLeft(InputAction.CallbackContext context)
        {
            left.value = context.ReadValue<float>();
        }
        public void OnRight(InputAction.CallbackContext context)
        {
            right.value = context.ReadValue<float>();
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            interact.value = context.ReadValue<float>();
        }
        class Button
        {
            public float value;
            public bool held, pressFrame, releaseFrame;
            public void UpdateValues()
            {
                if (value > 0)
                {
                    pressFrame = !held;

                    held = true;
                    releaseFrame = false;
                }
                else
                {
                    releaseFrame = held;

                    held = false;
                    pressFrame = false;
                }
            }
        }
    }
}
