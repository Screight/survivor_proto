using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_UpgradeFamilyData", menuName = "SurvivorProto/Upgrade/UpgradeFamilyData", order = 1)]
    public class UpgradeFamilyData : ScriptableObject
    {
        [SerializeField] List<UpgradeData> m_upgradeList;
        public List<UpgradeData> UpgradeList { get { return m_upgradeList; } }
    }
}
