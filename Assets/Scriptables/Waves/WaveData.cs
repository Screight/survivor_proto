using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_WaveData", menuName = "SurvivorProto/WaveData", order = 1)]
    public class WaveData : ScriptableObject
    {
        [SerializeField] List<EnemyWaveInfo> m_waveInfoList;
        [SerializeField] float m_frecuency;
        [Tooltip("Wave duration in seconds")]
        [SerializeField] int m_duration;

        public List<EnemyWaveInfo> WaveInfoList { get { return m_waveInfoList; } }
        public float Frecuency { get { return m_frecuency; } }
        public int Duration { get { return m_duration; } }
    }

    [System.Serializable]
    public struct EnemyWaveInfo
    {
        [SerializeField] EnemyData m_enemyData;
        [SerializeField] float m_chance;

        public EnemyData EnemyData { get { return m_enemyData; } }
        public float Chance { get { return m_chance; } }
    }
}
