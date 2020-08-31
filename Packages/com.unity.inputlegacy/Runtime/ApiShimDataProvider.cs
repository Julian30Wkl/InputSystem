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

        private static Key? ConvertKeyCodeToKey(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.Space: return Key.Space;
                case KeyCode.Return: return Key.Enter;
                case KeyCode.Tab: return Key.Tab;
                case KeyCode.BackQuote: return Key.Backquote;
                case KeyCode.Quote: return Key.Quote;
                case KeyCode.Semicolon: return Key.Semicolon;
                case KeyCode.Comma: return Key.Comma;
                case KeyCode.Period: return Key.Period;
                case KeyCode.Slash: return Key.Slash;
                case KeyCode.Backslash: return Key.Backslash;
                case KeyCode.LeftBracket: return Key.LeftBracket;
                case KeyCode.RightBracket: return Key.RightBracket;
                case KeyCode.Minus: return Key.Minus;
                case KeyCode.Equals: return Key.Equals;
                case KeyCode.A: return Key.A;
                case KeyCode.B: return Key.B;
                case KeyCode.C: return Key.C;
                case KeyCode.D: return Key.D;
                case KeyCode.E: return Key.E;
                case KeyCode.F: return Key.F;
                case KeyCode.G: return Key.G;
                case KeyCode.H: return Key.H;
                case KeyCode.I: return Key.I;
                case KeyCode.J: return Key.J;
                case KeyCode.K: return Key.K;
                case KeyCode.L: return Key.L;
                case KeyCode.M: return Key.M;
                case KeyCode.N: return Key.N;
                case KeyCode.O: return Key.O;
                case KeyCode.P: return Key.P;
                case KeyCode.Q: return Key.Q;
                case KeyCode.R: return Key.R;
                case KeyCode.S: return Key.S;
                case KeyCode.T: return Key.T;
                case KeyCode.U: return Key.U;
                case KeyCode.V: return Key.V;
                case KeyCode.W: return Key.W;
                case KeyCode.X: return Key.X;
                case KeyCode.Y: return Key.Y;
                case KeyCode.Z: return Key.Z;
                case KeyCode.Alpha1: return Key.Digit1;
                case KeyCode.Alpha2: return Key.Digit2;
                case KeyCode.Alpha3: return Key.Digit3;
                case KeyCode.Alpha4: return Key.Digit4;
                case KeyCode.Alpha5: return Key.Digit5;
                case KeyCode.Alpha6: return Key.Digit6;
                case KeyCode.Alpha7: return Key.Digit7;
                case KeyCode.Alpha8: return Key.Digit8;
                case KeyCode.Alpha9: return Key.Digit9;
                case KeyCode.Alpha0: return Key.Digit0;
                case KeyCode.LeftShift: return Key.LeftShift;
                case KeyCode.RightShift: return Key.RightShift;
                case KeyCode.LeftAlt: return Key.LeftAlt;
                case KeyCode.RightAlt: return Key.RightAlt;
                case KeyCode.LeftControl: return Key.LeftCtrl;
                case KeyCode.RightControl: return Key.RightCtrl;
                case KeyCode.LeftWindows: return Key.LeftMeta;
                case KeyCode.RightWindows: return Key.RightMeta;
                case KeyCode.AltGr: return Key.AltGr;
                case KeyCode.Menu: return Key.ContextMenu;
                case KeyCode.Escape: return Key.Escape;
                case KeyCode.LeftArrow: return Key.LeftArrow;
                case KeyCode.RightArrow: return Key.RightArrow;
                case KeyCode.UpArrow: return Key.UpArrow;
                case KeyCode.DownArrow: return Key.DownArrow;
                case KeyCode.Backspace: return Key.Backspace;
                case KeyCode.PageDown: return Key.PageDown;
                case KeyCode.PageUp: return Key.PageUp;
                case KeyCode.Home: return Key.Home;
                case KeyCode.End: return Key.End;
                case KeyCode.Insert: return Key.Insert;
                case KeyCode.Delete: return Key.Delete;
                case KeyCode.CapsLock: return Key.CapsLock;
                case KeyCode.Numlock: return Key.NumLock;
                case KeyCode.Print: return Key.PrintScreen;
                case KeyCode.ScrollLock: return Key.ScrollLock;
                case KeyCode.Pause: return Key.Pause;
                case KeyCode.KeypadEnter: return Key.NumpadEnter;
                case KeyCode.KeypadDivide: return Key.NumpadDivide;
                case KeyCode.KeypadMultiply: return Key.NumpadMultiply;
                case KeyCode.KeypadPlus: return Key.NumpadPlus;
                case KeyCode.KeypadMinus: return Key.NumpadMinus;
                case KeyCode.KeypadPeriod: return Key.NumpadPeriod;
                case KeyCode.KeypadEquals: return Key.NumpadEquals;
                case KeyCode.Keypad0: return Key.Numpad0;
                case KeyCode.Keypad1: return Key.Numpad1;
                case KeyCode.Keypad2: return Key.Numpad2;
                case KeyCode.Keypad3: return Key.Numpad3;
                case KeyCode.Keypad4: return Key.Numpad4;
                case KeyCode.Keypad5: return Key.Numpad5;
                case KeyCode.Keypad6: return Key.Numpad6;
                case KeyCode.Keypad7: return Key.Numpad7;
                case KeyCode.Keypad8: return Key.Numpad8;
                case KeyCode.Keypad9: return Key.Numpad9;
                case KeyCode.F1: return Key.F1;
                case KeyCode.F2: return Key.F2;
                case KeyCode.F3: return Key.F3;
                case KeyCode.F4: return Key.F4;
                case KeyCode.F5: return Key.F5;
                case KeyCode.F6: return Key.F6;
                case KeyCode.F7: return Key.F7;
                case KeyCode.F8: return Key.F8;
                case KeyCode.F9: return Key.F9;
                case KeyCode.F10: return Key.F10;
                case KeyCode.F11: return Key.F11;
                case KeyCode.F12: return Key.F12;
                // case KeyCode.OEM1: return Key.OEM1;
                // case KeyCode.OEM2: return Key.OEM2;
                // case KeyCode.OEM3: return Key.OEM3;
                // case KeyCode.OEM4: return Key.OEM4;
                // case KeyCode.OEM5: return Key.OEM5;
            }

            return null;
        }

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
                    var key = ConvertKeyCodeToKey(keyboardKeyCode);
                    return key.HasValue && ResolveState(Keyboard.current?[key.Value], request);
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
            return GetKey(KeyNames.NameToKey(keyCodeName), request);
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
                    t.phase = UnityEngine.TouchPhase.Began;
                    break;
                case InputSystem.TouchPhase.Moved:
                    t.phase = UnityEngine.TouchPhase.Moved;
                    break;
                case InputSystem.TouchPhase.Ended:
                    t.phase = UnityEngine.TouchPhase.Ended;
                    break;
                case InputSystem.TouchPhase.Canceled:
                    t.phase = UnityEngine.TouchPhase.Canceled;
                    break;
                case InputSystem.TouchPhase.Stationary:
                    t.phase = UnityEngine.TouchPhase.Stationary;
                    break;
            }

            t.type = TouchType.Direct; // ???
            t.pressure = f.pressure;
            t.maximumPossiblePressure = 1.0f; // seems to be normalized?
            t.radius = f.radius.magnitude; // ???

            return t;

            /*
             *
             *        private int m_FingerId;
        private Vector2 m_Position;
        private Vector2 m_RawPosition;
        private Vector2 m_PositionDelta;
        private float m_TimeDelta;
        private int m_TapCount;
        private TouchPhase m_Phase;
        private TouchType m_Type;
        private float m_Pressure;
        private float m_maximumPossiblePressure;
        private float m_Radius;
        private float m_RadiusVariance;
        private float m_AltitudeAngle;
        private float m_AzimuthAngle;

             *
             */
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