using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_SatelliteSkillData", menuName = "SurvivorProto/Skill/Satellite", order = 1)]
    public class SatelliteSkillData : ScriptableObject
    {
        [SerializeField] int m_damage;
        [SerializeField] float m_numberOfTurnsPerSecond;
        [SerializeField] int m_numberOfSatellites;
        [SerializeField] float m_size;
        [SerializeField] float m_radius;
        [SerializeField] float m_recoil;

        public int Damage { get { return m_damage; } }
        public float RotationalSpeed { get { return 360 * m_numberOfTurnsPerSecond; } }
        public int NumberOfSatellites { get { return m_numberOfSatellites; } }
        public float Size { get { return m_size; } }
        public float Radius { get { return m_radius; } }
        public float Recoil { get { return m_recoil; } }
    }
}