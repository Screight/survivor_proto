using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_SpawnLevelData", menuName = "SurvivorProto/SpawnLevelData", order = 1)]
    public class SpawnLevelData : ScriptableObject
    {
        [SerializeField] List<WaveData> m_waveDataList;

        public List<WaveData> WaveDataList { get { return m_waveDataList; } }
    }
}
