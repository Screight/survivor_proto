using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class UpgradeController
    {
        Dictionary<UpgradeFamilyData, int> m_upgradeFamilyLevel;

        UpgradeWindowModel m_upgradeWindowModel;
        int m_selectedUpgradeDataIndex;

        List<UpgradeFamilyData> m_upgradeList;

        public UpgradeController()
        {
            m_upgradeFamilyLevel = new Dictionary<UpgradeFamilyData, int>();
            GameManager.Instance.GameData.UpgradeFamilyList.ForEach((UpgradeFamilyData data) =>
            {
                if (!m_upgradeFamilyLevel.ContainsKey(data)) { m_upgradeFamilyLevel.Add(data, 0); }
            });

            m_upgradeWindowModel = GUIManager.Instance.UpgradeWindowModel;
        }

        List<UpgradeFamilyData> GetAvailableUpgradeFamily()
        {
            List<UpgradeFamilyData> upgradeFamilies = new List<UpgradeFamilyData>();

            foreach (KeyValuePair<UpgradeFamilyData, int> pair in m_upgradeFamilyLevel)
            {
                if (pair.Value < pair.Key.UpgradeList.Count) { upgradeFamilies.Add(pair.Key); }
            }
            return upgradeFamilies;
        }

        public void SetUpUpgradeMenu()
        {
            List<UpgradeFamilyData> upgradeFamilyDataList = GetAvailableUpgradeFamily();

            m_upgradeList = new List<UpgradeFamilyData>();
            List<UpgradeData> upgradeDataList = new List<UpgradeData>();

            if(upgradeFamilyDataList.Count == 0)
            {
                m_upgradeWindowModel.GameObject.SetActive(false);
                if (!PlayerController.Instance.Stats.CheckIfLevelUp()) { Time.timeScale = 1; }
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                int index = Random.Range(0, upgradeFamilyDataList.Count);
                UpgradeFamilyData upgradeFamily = upgradeFamilyDataList[index];

                m_upgradeList.Add(upgradeFamily);
                int level = m_upgradeFamilyLevel[upgradeFamily];
                UpgradeData upgradeData = upgradeFamily.UpgradeList[level];

                upgradeDataList.Add(upgradeData);
                if (upgradeFamilyDataList.Count > 1) { upgradeFamilyDataList.RemoveAt(index); }
            }

            m_upgradeWindowModel.SetUp(upgradeDataList);
            SelectUpgrade(0);
        }

        public void SelectUpgrade(int p_index)
        {
            m_selectedUpgradeDataIndex = p_index;
            m_upgradeWindowModel.HighlightUpgradeIcon(p_index);
        }

        public void SetSelectedBoxToSelectedUpgrade () {
            m_upgradeWindowModel.SetUpgradeIconToSelectBox(m_selectedUpgradeDataIndex);
        }

        public void ChooseSelectedUpgrade()
        {
            UpgradeFamilyData familyData = m_upgradeList[m_selectedUpgradeDataIndex];
            int level = m_upgradeFamilyLevel[familyData];

            UpgradeData data = familyData.UpgradeList[level];

            data.OnCollectUpgrade();
            m_upgradeFamilyLevel[familyData]++;
            m_upgradeWindowModel.GameObject.SetActive(false);
            if (!PlayerController.Instance.Stats.CheckIfLevelUp()) { Time.timeScale = 1; }
        }

    }
}