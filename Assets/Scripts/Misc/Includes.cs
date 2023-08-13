using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_STATES { DEFAULT}

public struct PositionTr
{
    Transform m_tr;
    Vector2? m_pos;

    public PositionTr(Transform p_tr, Vector2? p_pos)
    {
        m_tr = p_tr;
        m_pos = p_pos;
    }

    public Vector2 GetPosition()
    {
        if (m_tr == null) { return m_pos.Value; }
        else { return m_tr.position; }
    }

    public void SetTransform(Transform p_transform) { m_tr = p_transform; }
    public void SetPosition(Vector2 p_position) { m_pos = p_position; }

    public Transform Transform { get { return m_tr; } }
    public Vector2 Position { get { return m_pos.Value; } }
}