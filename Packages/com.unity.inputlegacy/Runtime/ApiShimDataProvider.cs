using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputLegacy.OldInputCompatibility
{
    internal class ApiShimDataProvider : Input.DataProvider
    {
        public void OnTextChange(char x)
        {
            if (inputStringStep != InputUpdate.s_UpdateStepCount)
            {
                inputStringData = x.ToString();
                inputStringStep = InputUpdate.s_UpdateStepCount;
            }
            else
                inputStringData += x.ToString();
        }

        public ApiShimDataProvider(
            /*
            InputActionMap setMap,
            IDictionary<string, ActionStateListener> setStateListeners,
            ActionStateListener[] setKeyActions
            */
        )
        {
            InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
            // map = setMap;
            // stateListeners = setStateListeners;
            // keyActions = setKeyActions;
        }

        //private InputActionMap map;
        //private IDictionary<string, ActionStateListener> stateListeners; // TODO remove this later on
        //private ActionStateListener[] keyActions; // array of keycodes

        private string inputStringData = "";
        private uint inputStringStep = 0;

        private static bool ResolveState(InputSystem.Controls.ButtonControl control, Request request)
        {
            if (control == null)
                return false;

            switch (request)
            {
                case Request.Pressed:
                    return control.isPressed;
                case Request.PressedThisFrame:
                    return control.wasPressedThisFrame;
                case Request.ReleasedThisFrame:
                    return control.wasReleasedThisFrame;
                default:
                    return false;
            }
        }

        // private static bool ResolveState(ActionStateListener stateListener, StateRequest request)
        // {
        //     if (stateListener == null)
        //         return false;
        //
        //     switch (request)
        //     {
        //         case StateRequest.Pressed:
        //             return stateListener.isPressed;
        //         case StateRequest.PressedThisFrame:
        //             return stateListener.action.triggered;
        //         case StateRequest.ReleasedThisFrame:
        //             return stateListener.cancelled;
        //         default:
        //             return false;
        //     }
        // }

        public override float GetAxis(string axisName)
        {
            //var actionName = ActionNameMapper.GetAxisActionNameFromAxisName(axisName);
            //return stateListeners.TryGetValue(actionName, out var listener) ? listener.action.ReadValue<float>() : 0.0f;

            // TODO
            return 0.0f;
        }

        public override bool GetButton(string axisName, Request request)
        {
            //var actionName = ActionNameMapper.GetAxisActionNameFromAxisName(axisName);
            //return stateListeners.TryGetValue(actionName, out var listener) && ResolveState(listener, stateRequest);

            // TODO
            return false;
        }

        public override bool GetKey(KeyCode keyCode, Request request)
        {
            switch (keyCode)
            {
                case var keyboardKeyCode when (keyCode >= KeyCode.None && keyCode <= KeyCode.Menu):
                {
                    var key = KeyCodeMapping.KeyCodeToKeyboardKey(keyboardKeyCode);
                    return key != Key.None && ResolveState(Keyboard.current?[key], request);
                }

                case var mouseKeyCode when (keyCode >= KeyCode.Mouse0 && keyCode <= KeyCode.Mouse6):
                {
                    switch (mouseKeyCode)
                    {
                        case KeyCode.Mouse0: return ResolveState(Mouse.current?.leftButton, request);
                        case KeyCode.Mouse1: return ResolveState(Mouse.current?.rightButton, request);
                        case KeyCode.Mouse2: return ResolveState(Mouse.current?.middleButton, request);
                        ////REVIEW: With these two, is it this way around or the other?
                        case KeyCode.Mouse3: return ResolveState(Mouse.current?.forwardButton, request);
                        case KeyCode.Mouse4: return ResolveState(Mouse.current?.backButton, request);
                        // TODO KeyCode.Mouse5 / KeyCode.Mouse6
                        default:
                            return false;
                    }
                    return false;
                }

                case var joystickKeyCode when (keyCode >= KeyCode.JoystickButton0 && keyCode <= KeyCode.Joystick8Button19):
                {
                    // TODO
                    return false;
                }

                default:
                    return false;
            }
        }

        public override bool GetKey(string keyCodeName, Request request)
        {
            return GetKey(KeyCodeMapping.KeyNameToKeyCode(keyCodeName), request);
        }

        public override bool IsAnyKey(Request request)
        {
            return ResolveState(Keyboard.current?.anyKey, request);
        }

        public override string GetInputString()
        {
            if (inputStringStep == InputUpdate.s_UpdateStepCount)
                return inputStringData;
            return "";
        }

        public override bool IsMousePresent()
        {
            return Mouse.current != null;
        }

        public override Vector3 GetMousePosition()
        {
            // seems like Z is always 0.0f
            return Mouse.current != null
                ? new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0.0f)
                : Vector3.zero;
        }

        public override Vector2 GetMouseScrollDelta()
        {
            return Mouse.current != null
                ? new Vector2(Mouse.current.scroll.x.ReadValue(), Mouse.current.scroll.y.ReadValue())
                : Vector2.zero;
        }

        public override Touch GetTouch(int index)
        {
            if (index >= InputSystem.EnhancedTouch.Touch.activeTouches.Count)
                return new Touch();

            var t = new Touch();
            var f = InputSystem.EnhancedTouch.Touch.activeTouches[index];
            t.fingerId = f.touchId; // ???
            t.position = f.screenPosition;
            t.rawPosition = f.screenPosition; // ???
            t.deltaPosition = f.delta;
            t.deltaTime = (float)f.time; // ???
            t.tapCount = f.tapCount;
            switch (f.phase)
            {
                case InputSystem.TouchPhase.None:
                case InputSystem.TouchPhase.Began:
                    t.phase = TouchPhase.Began;
                    break;
                case InputSystem.TouchPhase.Moved:
                    t.phase = TouchPhase.Moved;
                    break;
                case InputSystem.TouchPhase.Ended:
                    t.phase = TouchPhase.Ended;
                    break;
                case InputSystem.TouchPhase.Canceled:
                    t.phase = TouchPhase.Canceled;
                    break;
                case InputSystem.TouchPhase.Stationary:
                    t.phase = TouchPhase.Stationary;
                    break;
            }

            t.type = TouchType.Direct; // ???
            t.pressure = f.pressure;
            t.maximumPossiblePressure = 1.0f; // seems to be normalized?
            t.radius = f.radius.magnitude; // ???
            t.radiusVariance = 0.0f; // ???
            t.altitudeAngle = 0.0f; // ???
            t.azimuthAngle = 0.0f; // ???
            return t;
        }

        public override int GetTouchCount()
        {
            return InputSystem.EnhancedTouch.Touch.activeTouches.Count;
        }

        public override bool GetTouchPressureSupported()
        {
            return true; // ???
        }

        public override bool GetStylusTouchSupported()
        {
            return false;
        }

        public override bool GetTouchSupported()
        {
            return true; // ???
        }

        public override void SetMultiTouchEnabled(bool enable)
        {
        }

        public override bool GetMultiTouchEnabled()
        {
            return true; // ??
        }
    };
}