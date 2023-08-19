using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_LightningBarrierSkillData", menuName = "SurvivorProto/Skill/LightningBarrier", order = 1)]
    public class LightningBarrierSkillData : ScriptableObject, ISkill
    {
        [SerializeField] int m_damage;
        [SerializeField] float m_damageInterval;

        public int Damage { get { return m_damage; } }
        public float DamageInterval { get { return m_damageInterval; } }
    }
}