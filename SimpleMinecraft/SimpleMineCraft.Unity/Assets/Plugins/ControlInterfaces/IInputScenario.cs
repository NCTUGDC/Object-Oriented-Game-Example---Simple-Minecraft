﻿using System;
using UnityEngine;

namespace SimpleMinecraft.Unity.SystemScripts
{
    public interface IInputScenario
    {
        event Action<KeyCode> OnKeyDown;
        event Action<KeyCode> OnKeyUp;
    }
}