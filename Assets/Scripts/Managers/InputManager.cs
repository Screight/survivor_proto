using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace SurvivorProto
{
    public enum ACTION_BUTTON { ACTION, INTERACT, LAST_NO_USE }
    public class InputManager : Singleton<InputManager>
    {
        InputActions m_inputActions;

        Vector2 m_movementInput;
        Vector2 m_mousePosition;

        Dictionary<ACTION_BUTTON, float> m_buttonToPressedTime;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            m_inputActions = new InputActions();
            m_inputActions.Player.Enable();

            m_buttonToPressedTime = new Dictionary<ACTION_BUTTON, float>();
        }

        private void Start()
        {
            m_buttonToPressedTime.Add(ACTION_BUTTON.ACTION, -1);
            m_buttonToPressedTime.Add(ACTION_BUTTON.INTERACT, -1);
        }

        private void OnEnable()
        {
            m_inputActions.Player.MoveHorizontal.performed += (m_inputActions) =>
            {
                m_movementInput.x = m_inputActions.ReadValue<float>();
            };
            m_inputActions.Player.MoveHorizontal.canceled += (m_inputActions) =>
            {
                m_movementInput.x = 0;
            };
            m_inputActions.Player.MoveVertical.performed += (m_inputActions) =>
            {
                m_movementInput.y = m_inputActions.ReadValue<float>();
            };
            m_inputActions.Player.MoveVertical.canceled += (m_inputActions) =>
            {
                m_movementInput.y = 0;
            };

            m_inputActions.Player.MousePosition.performed += (m_inputActions) =>
            {
                m_mousePosition = m_inputActions.ReadValue<Vector2>();
            };
        }

        private void Update()
        {
            for (int i = 0; i < (int)ACTION_BUTTON.LAST_NO_USE; i++)
            {
                if (!m_buttonToPressedTime.ContainsKey((ACTION_BUTTON)i)) { continue; }
                if (m_buttonToPressedTime[(ACTION_BUTTON)i] >= 0)
                {
                    m_buttonToPressedTime[(ACTION_BUTTON)i] += Time.deltaTime;
                }
            }

            InputSystem.Update();
        }

        public Vector2 MovementInput { get { return m_movementInput; } }
        public Vector2 RawMovementInput
        {
            get
            {
                Vector2 resultVector = new Vector2();
                if (m_movementInput.x < 0.1f && m_movementInput.x > -0.1f) { resultVector.x = 0; }
                else {
                    if (m_movementInput.x < 0) { resultVector.x = -1; }
                    else if(m_movementInput.x > 0) { resultVector.x = 1; }
                }
                if (m_movementInput.y < 0.1f && m_movementInput.y > -0.1f) { resultVector.y = 0; }
                else {
                    if (m_movementInput.y < 0) { resultVector.y = -1; }
                    else if (m_movementInput.y > 0) { resultVector.y = 1; }
                }
                return resultVector;
            }
        }
        public Vector2 MousePosition { get { return m_mousePosition; } }
        public bool IsButtonPressed(ACTION_BUTTON p_button)
        {
            return m_buttonToPressedTime[p_button] == 0;
        }
        public bool IsButtonDown(ACTION_BUTTON p_button) { return m_buttonToPressedTime[p_button] >= 0; }
    }
}

