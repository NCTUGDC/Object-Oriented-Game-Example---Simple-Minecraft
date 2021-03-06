﻿using SimpleMinecraft.Unity.Scripts.SystemScripts;
using UnityEngine;

namespace SimpleMinecraft.Unity.Scripts.PlayerScripts
{
    public class PlayerMoveController : MonoBehaviour
    {
        private Rigidbody selfRigidbody;
        void Start()
        {
            selfRigidbody = GetComponent<Rigidbody>();
            InputManager.Instance.OnKeyDown += OnJumpKeyDown;
            InputManager.Instance.OnKeyDown += OnMoveKeyDown;
            InputManager.Instance.OnKeyPress += OnRotateKeyPress;
            InputManager.Instance.OnKeyUp += OnMoveKeyUp;
        }

        void Update()
        {
            PlayerManager.Instance.PlayerFrontPosition = transform.localPosition + transform.forward;
            if (selfRigidbody.position.y < SceneManager.Instance.ResetPositionY)
            {
                selfRigidbody.MovePosition(SceneManager.Instance.OriginPoint);
            }
        }

        void OnJumpKeyDown(KeyCode keyCode)
        {
            if(keyCode == KeyCode.Space)
            {
                selfRigidbody.AddForce(new Vector3(0, 300, 0));
            }
        }
        void OnMoveKeyDown(KeyCode keyCode)
        {
            switch(keyCode)
            {
                case KeyCode.W:
                    selfRigidbody.velocity = selfRigidbody.transform.forward * 10;
                    break;
                case KeyCode.S:
                    selfRigidbody.velocity = selfRigidbody.transform.forward * -10;
                    break;
            }
        }
        void OnRotateKeyPress(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.A:
                    selfRigidbody.transform.Rotate(new Vector3(0, -3, 0));
                    break;
                case KeyCode.D:
                    selfRigidbody.transform.Rotate(new Vector3(0, 3, 0));
                    break;
            }
        }
        void OnMoveKeyUp(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.W:
                case KeyCode.S:
                    Vector3 velocity = selfRigidbody.velocity;
                    velocity.x = 0;
                    velocity.z = 0;
                    selfRigidbody.velocity = velocity;
                    break;
            }
        }
    }
}

