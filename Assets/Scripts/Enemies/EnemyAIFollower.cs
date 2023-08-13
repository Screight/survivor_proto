using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class EnemyAIFollower : EnemyAI
    {
        Rigidbody2D m_rb;
        SteeringFollow m_steeringFollow;
        public EnemyAIFollower(Enemy p_enemy, Rigidbody2D p_rb)
        {
            m_enemy = p_enemy;
            m_rb = p_rb;

            m_steeringFollow = new SteeringFollow(m_enemy.transform, new PositionTr(PlayerController.Instance.transform, null), m_enemy.Stats.Speed);
        }   

        public override void HandleAI()
        {
            m_steeringFollow.UpdatePosition(m_rb);
        }
    }
}
