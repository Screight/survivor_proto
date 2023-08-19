using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SurvivorProto
{
    public class GUIManager : Singleton<GUIManager>
    {
        [SerializeField] GameObject m_upgradeIconPrefab;
        UpgradeWindowModel m_upgradeWindowModel;
        [SerializeField] TMPro.TextMeshProUGUI m_levelTimerTMP;

        [Header("Weapon")]
        [SerializeField] Image m_reloadBarFillIMG;
        [SerializeField] TMPro.TextMeshProUGUI m_currentAmmoTargetTMP;
        [SerializeField] TMPro.TextMeshProUGUI m_currentAmmoTMP;

        [Header("Experience")]
        [SerializeField] TMPro.TextMeshProUGUI m_levelTMP;
        [SerializeField] Image m_experienceFillIMG;

        [Header("Health")]
        [SerializeField] GameObject m_healthIconPrefab;
        [SerializeField] Transform m_healthIconParentTr;
        List<Image> m_healthIMGList;

        protected override void Awake()
        {
            base.Awake();
            m_levelTMP.text = "Level  1";
            m_experienceFillIMG.fillAmount = 0;
            m_healthIMGList = new List<Image>();
        }

        private void Start()
        {
            m_upgradeWindowModel = new UpgradeWindowModel(transform.Find("UpgradeWindow").gameObject, m_upgradeIconPrefab);
            m_upgradeWindowModel.GameObject.SetActive(false);

            InstantiateHealth((int)PlayerController.Instance.Stats.MaxHealth);
        }
        public void AddMaxHealth(int p_value, bool p_areNewHealthFull = true) { InstantiateHealth(p_value, p_areNewHealthFull = true); }
        public void SetHealthTo(int p_value, bool p_areNewHealthFull = true)
        {
            GUIData data = GameManager.Instance.GUIData;
            for (int i = 0; i < m_healthIMGList.Count; i++)
            {
                m_healthIMGList[i].sprite = i < p_value ? data.FullHealthSprite : data.EmptyHealthSprite;
            }

            if (p_value < m_healthIMGList.Count) { InstantiateHealth(p_value - m_healthIMGList.Count, p_areNewHealthFull); }
        }

        void InstantiateHealth(int p_value, bool p_areNewHealthFull = true)
        {
            if(p_value < 0) { return; }
            GUIData data = GameManager.Instance.GUIData;
            for (int i = 0; i < p_value; i++)
            {
                m_healthIMGList.Add(
                    Instantiate(m_healthIconPrefab, m_healthIconParentTr).GetComponent<Image>()
                    );
                m_healthIMGList[m_healthIMGList.Count - 1].sprite = p_areNewHealthFull ? data.FullHealthSprite : data.EmptyHealthSprite;
            }
        }
        public void SetLevelTimerTo(int p_minutes, int p_seconds) {
            string secondsTxt = p_seconds.ToString();
            if(secondsTxt.Length == 1) { secondsTxt = "0" + secondsTxt; }
            m_levelTimerTMP.text = p_minutes + ":" + secondsTxt;
        }
        public void SetLevelTo(int p_level) { m_levelTMP.text = "Level  " + p_level; }

        public void SetExpFillTo(float p_percentage) { m_experienceFillIMG.fillAmount = p_percentage; }

        public void SetCurrentAmmoTo(int p_ammo, int p_maxAmmo) {
            if(p_ammo < 0) { p_ammo = 0; }
            m_currentAmmoTargetTMP.text = p_ammo.ToString();
            string curAmmoTxt = p_ammo.ToString();
            while(curAmmoTxt.Length < 3) { curAmmoTxt = "0" + curAmmoTxt; }
            string maxAmmoTxt = p_maxAmmo.ToString();
            while (maxAmmoTxt.Length < 3) { maxAmmoTxt = "0" + maxAmmoTxt; }

            m_currentAmmoTMP.text = curAmmoTxt + "/" + maxAmmoTxt;
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
