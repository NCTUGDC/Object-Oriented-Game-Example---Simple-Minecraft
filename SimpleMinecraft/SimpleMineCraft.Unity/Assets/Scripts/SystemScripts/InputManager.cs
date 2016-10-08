using SimpleMinecraft.Unity.SystemScripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleMinecraft.Unity.Scripts.SystemScripts
{
    public class InputManager : EventTrigger, IInputScenario
    {
        public static InputManager Instance { get; private set; }

        private event Action<KeyCode> onKeyDown;
        public event Action<KeyCode> OnKeyDown { add { onKeyDown += value; } remove { onKeyDown -= value; } }

        private event Action<KeyCode> onKeyUp;
        public event Action<KeyCode> OnKeyUp { add { onKeyUp += value; } remove { onKeyUp -= value; } }

        private event Action<KeyCode> onKeyPress;
        public event Action<KeyCode> OnKeyPress { add { onKeyPress += value; } remove { onKeyPress -= value; } }

        private event Action<int> onMouseButtonUp;
        public event Action<int> OnMouseButtonUp { add { onMouseButtonUp += value; } remove { onMouseButtonUp -= value; } }

        private List<KeyCode> keyCodes;
        private List<int> mouseButtons;

        public InputManager()
        {
            keyCodes = new List<KeyCode>
            {
                KeyCode.Space,
                KeyCode.A,
                KeyCode.S,
                KeyCode.D,
                KeyCode.W,
                KeyCode.I,
            };
            mouseButtons = new List<int>
            {
                0,
                1
            };
        }

        void Awake()
        {
            Instance = this;
        }
        void Update()
        {
            foreach (KeyCode key in keyCodes)
            {
                if (Input.GetKeyDown(key) && onKeyDown != null)
                {
                    onKeyDown.Invoke(key);
                }
                if (Input.GetKeyUp(key) && onKeyUp != null)
                {
                    onKeyUp.Invoke(key);
                }
                if(Input.GetKey(key) && onKeyPress != null)
                {
                    onKeyPress.Invoke(key);
                }
            }
            foreach (int mouseButton in mouseButtons)
            {
                if (Input.GetMouseButtonUp(mouseButton) && onMouseButtonUp != null)
                {
                    onMouseButtonUp.Invoke(mouseButton);
                }
            }
        }
    }
}
