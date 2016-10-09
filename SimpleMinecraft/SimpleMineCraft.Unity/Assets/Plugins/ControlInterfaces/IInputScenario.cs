using System;
using UnityEngine;

namespace SimpleMinecraft.Unity.ControllerInterfaces
{
    public interface IInputScenario
    {
        event Action<KeyCode> OnKeyDown;
        event Action<KeyCode> OnKeyUp;
        event Action<KeyCode> OnKeyPress;

        event Action<int> OnMouseButtonUp;
    }
}
