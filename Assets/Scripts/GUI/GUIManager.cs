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
    }
}
