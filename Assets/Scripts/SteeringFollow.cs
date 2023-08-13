using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringFollow
{
    float m_speed;
    float m_slowdownDistance = 0.1f;

    Vector2 m_velocity = Vector2.zero;

    Transform m_entityTr;
    PositionTr m_targetTr;

    public SteeringFollow(Transform p_entityTr, PositionTr p_targetTr, float p_speed)
    {
        m_entityTr = p_entityTr;
        m_targetTr = p_targetTr;
        m_speed = p_speed;
    }

    public void UpdatePosition(Rigidbody2D p_rb) {

        Vector2 targetPos = m_targetTr.GetPosition();

        Vector2 desiredDir = (targetPos - (Vector2)m_entityTr.position).normalized;
        Vector2 desiredVel = m_speed * desiredDir;

        Vector2 steering = desiredVel - m_velocity;

        m_velocity = p_rb.velocity;
        m_velocity += steering * Time.deltaTime;

        float slowDownFactor = Mathf.Clamp01((targetPos - (Vector2)m_entityTr.position).magnitude / m_slowdownDistance);
        m_velocity *= slowDownFactor;

        //m_entityTr.position += (Vector3)m_velocity * Time.deltaTime;
        p_rb.velocity = m_velocity;
    }

    public Transform EntityTr { get { return m_entityTr; } }
    public void SetTargetPos(Transform p_tr) { m_targetTr.SetTransform(p_tr); }
    public void SetTargetPos(Vector2 p_pos) { m_targetTr.SetPosition(p_pos); }
    public void SetTargetPos(PositionTr p_posTr) { m_targetTr = p_posTr; }
    public Vector2 Velocity { get { return m_velocity; } set { m_velocity = value; } }
    public float Speed { get { return m_speed; } set { m_speed = value; } }

}
