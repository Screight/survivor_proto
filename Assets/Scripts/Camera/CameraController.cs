using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] CameraData m_data;



        Transform m_target;
        Vector2 m_velocity;

        Vector2 m_targetPos;

        private void Start()
        {
            m_target = PlayerController.Instance.transform;
        }

        private void LateUpdate()
        {
            Vector2 lookAheadFactor = Vector2.zero;

            if (PlayerController.Instance.Movement.x > 0) { lookAheadFactor.x = m_data.LookAhead; }
            else if (PlayerController.Instance.Movement.x < 0) { lookAheadFactor.x = -m_data.LookAhead; }
            if (PlayerController.Instance.Movement.y > 0) { lookAheadFactor.y = m_data.LookAhead; }
            else if (PlayerController.Instance.Movement.y < 0) { lookAheadFactor.y = -m_data.LookAhead; }

            m_targetPos = m_target.position;

            m_targetPos += lookAheadFactor;
            Vector3 resultVector = Vector2.SmoothDamp(transform.position, m_targetPos, ref m_velocity, m_data.Smoothing);
            resultVector.z = transform.position.z;
            transform.position = resultVector;
            Debug.DrawLine(transform.position, m_target.position);
        }
    }
}