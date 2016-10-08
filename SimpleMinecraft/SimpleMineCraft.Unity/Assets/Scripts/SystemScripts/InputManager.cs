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

        private List<KeyCode> keyCodes;

        void Awake()
        {
            Instance = this;
            keyCodes = new List<KeyCode>
            {
                KeyCode.Space,
                KeyCode.A,
                KeyCode.S,
                KeyCode.D,
                KeyCode.W,
                KeyCode.I,
            };
        }

        void Update()
        {
            foreach (KeyCode key in keyCodes)
            {
                if (Input.GetKeyDown(key) && onKeyDown != null)
                    onKeyDown.Invoke(key);
                if (Input.GetKeyUp(key) && onKeyUp != null)
                {
                    onKeyUp.Invoke(key);
                }
            }
        }
    }
}
