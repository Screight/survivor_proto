using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SurvivorProto
{
    public class UpgradeIconInteractive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        UpgradeController m_upgradeController;
        UpgradeWindowModel m_upgradeWindowModel;
        int m_childIndex;

        float m_scaleOnHover;

        private void Start()
        {
            m_upgradeController = LevelManager.Instance.UpgradeController;
            m_upgradeWindowModel = GUIManager.Instance.UpgradeWindowModel;

            Transform parentTr = transform.parent;
            for(int i = 0; i < parentTr.childCount; i++)
            {
                if(parentTr.GetChild(i) == transform) { m_childIndex = i; break; }
            }
            m_scaleOnHover = GameManager.Instance.GUIData.UpgradeIconScaleOnMouseOver;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_upgradeWindowModel.SetUpgradeIconToSelectBox(m_childIndex);
            // SHOULD NOT BE HARDCODED
            transform.localScale = m_scaleOnHover * Vector3.one;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
            m_upgradeController.SetSelectedBoxToSelectedUpgrade();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_upgradeController.SelectUpgrade(m_childIndex);
        }
    }
}