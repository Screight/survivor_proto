using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivorProto
{
    public class UpgradeWindowModel
    {
        GameObject m_gO;

        TMPro.TextMeshProUGUI m_upgradeTitleTMP;
        TMPro.TextMeshProUGUI m_upgradeDescriptionTMP;

        List<UpgradeIconModel> m_upgradeIconModelList;

        int m_numberOfUpgrades;

        public UpgradeWindowModel(GameObject p_gO, GameObject p_upgradeIconPrefab)
        {
            m_gO = p_gO;

            Transform parentTr = m_gO.transform.Find("SelectionBox");

            m_upgradeTitleTMP = parentTr.Find("UpgradeTitleTMP").GetComponent<TMPro.TextMeshProUGUI>();
            m_upgradeDescriptionTMP = parentTr.Find("UpgradeDescriptionTMP").GetComponent<TMPro.TextMeshProUGUI>();

            m_numberOfUpgrades = GameManager.Instance.GameData.NumberOfUpgradesToChooseFrom;

            m_upgradeIconModelList = new List<UpgradeIconModel>();
            for (int i = 0; i < m_numberOfUpgrades; i++)
            {
                m_upgradeIconModelList.Add(new UpgradeIconModel(MonoBehaviour.Instantiate(p_upgradeIconPrefab, m_gO.transform.Find("UpgradeList"))));
            }

            m_gO.transform.Find("ChooseUpgradeBTN").GetComponent<Button>().onClick.AddListener(() => {
                GUIManager.Instance.CurrentAmmoTarget.SetActive(true);
                LevelManager.Instance.UpgradeController.ChooseSelectedUpgrade();
            });
        }

        public GameObject GameObject { get { return m_gO; } }

        public void SetUp(List<UpgradeData> p_upgradesList)
        {
            m_gO.SetActive(true);
            for(int i = 0; i < p_upgradesList.Count && i < m_numberOfUpgrades; i++)
            {
                m_upgradeIconModelList[i].IconIMG.sprite = p_upgradesList[i].Icon;
                m_upgradeIconModelList[i].Data = p_upgradesList[i];
            }
            SetUpgradeIconToSelectBox(0);
        }

        public void SetUpgradeIconToSelectBox(int p_index)
        {
            for (int i = 0; i < m_upgradeIconModelList.Count; i++)
            {
                UpgradeIconModel model = m_upgradeIconModelList[i];

                if(i == p_index)
                {
                    m_upgradeTitleTMP.text = model.Data.Name;
                    m_upgradeDescriptionTMP.text = model.Data.Description;
                }
            }
        }

        public void HighlightUpgradeIcon(int p_index)
        {
            for (int i = 0; i < m_upgradeIconModelList.Count; i++)
            {
                UpgradeIconModel model = m_upgradeIconModelList[i];
                model.OutLineIMG.enabled = i == p_index;
            }
        }
    }

    public class UpgradeIconModel
    {
        GameObject m_gO;
        Image m_outLineIMG;
        Image m_iconIMG;
        UpgradeData m_data;

        public UpgradeIconModel(GameObject p_gO)
        {
            m_gO = p_gO;
            m_outLineIMG = m_gO.GetComponent<Image>();
            m_iconIMG = m_gO.transform.GetChild(0).GetComponent<Image>();
        }

        public Image OutLineIMG { get { return m_outLineIMG; } }
        public Image IconIMG { get { return m_iconIMG; } }
        public UpgradeData Data { get { return m_data; } set { m_data = value; } }
        public GameObject GameObject { get { return m_gO; } }
    }
}