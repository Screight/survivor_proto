using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivorProto
{
    public class GUIManager : Singleton<GUIManager>
    {
        [SerializeField] Image m_reloadBarFillIMG;
        [SerializeField] TMPro.TextMeshProUGUI m_currentAmmoTMP;

        [SerializeField] TMPro.TextMeshProUGUI m_levelTMP;
        [SerializeField] Image m_experienceFillIMG;

        [SerializeField] GameObject m_upgradeIconPrefab;
        UpgradeWindowModel m_upgradeWindowModel;

        protected override void Awake()
        {
            base.Awake();
            m_levelTMP.text = "Level  1";
            m_experienceFillIMG.fillAmount = 0;

        }

        private void Start()
        {
            m_upgradeWindowModel = new UpgradeWindowModel(transform.Find("UpgradeWindow").gameObject, m_upgradeIconPrefab);
            m_upgradeWindowModel.GameObject.SetActive(false);
        }

        public void SetLevelTo(int p_level) { m_levelTMP.text = "Level  " + p_level; }

        public void SetExpFillTo(float p_percentage) { m_experienceFillIMG.fillAmount = p_percentage; }

        public void SetCurrentAmmoTo(int p_ammo) {
            if(p_ammo < 0) { p_ammo = 0; }
            m_currentAmmoTMP.text = p_ammo.ToString();
        }
        public void SetReloadBarTo(bool p_isActive) { m_reloadBarFillIMG.gameObject.transform.parent.gameObject.SetActive(p_isActive); }
        public void SetReloadBarFillTo(float p_fillAmount)
        {
            p_fillAmount = Mathf.Clamp01(p_fillAmount);
            m_reloadBarFillIMG.fillAmount = p_fillAmount;
        }
        public UpgradeWindowModel UpgradeWindowModel { get { return m_upgradeWindowModel; } }
    }
}
