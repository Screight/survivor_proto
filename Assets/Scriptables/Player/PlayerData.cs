using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_PlayerData", menuName = "SurvivorProto/PlayerData", order = 1)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] float m_movementSpeed;
        [SerializeField] int m_health;
        [SerializeField] float m_healthRegen;

        [SerializeField] float m_collectionRange;

        [SerializeField] float m_repulseOnHitRange;
        [SerializeField] float m_repulseOnHitForce;

        List<UpgradeFamilyData> m_upgradeFamilyList;

        [SerializeField] AudioClip m_onHitAC;

        public AudioClip OnHitAC { get { return m_onHitAC; } }
        public float HealthRegen { get { return m_healthRegen; } }
        public float MovementSpeed { get { return m_movementSpeed; } }
        public int Health { get { return m_health; } }
        public float CollectionRange { get { return m_collectionRange; } }
        public float RepulseOnHitRange { get { return m_repulseOnHitRange; } }
        public float RepulseOnHitForce { get { return m_repulseOnHitForce; } }
        public List<UpgradeFamilyData> UpgradeFamilyList { get { return m_upgradeFamilyList; } }
    }
}
