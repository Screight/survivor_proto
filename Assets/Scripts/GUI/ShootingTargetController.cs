using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class ShootingTargetController : MonoBehaviour
    {
        Camera m_camera;
        InputManager m_inputManager;

        private void Start()
        {
            m_camera = Camera.main;
            m_inputManager = InputManager.Instance;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
            HandleTargetMovementWithMouse();
        }

        void HandleTargetMovementWithMouse()
        {
            Vector2 position = m_inputManager.MousePosition;

            // Constrict the target to be inside the screen
            if (position.x < 0) { position.x = 0; }
            else if (position.x > Screen.width) { position.x = Screen.width; }

            if (position.y < 0) { position.y = 0; }
            else if (position.y > Screen.height) { position.y = Screen.height; }

            transform.position = position;
        }

    }
}
