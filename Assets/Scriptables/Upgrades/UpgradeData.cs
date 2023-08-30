using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_UpgradeData", menuName = "SurvivorProto/Upgrade/UpgradeData", order = 1)]
    public class UpgradeData : ScriptableObject
    {
        [SerializeField] Sprite m_icon;
        [SerializeField] uint m_nameLocalisationID;

        [SerializeField] List<UpgradeComponentData> m_upgradesList;

        public void OnCollectUpgrade()
        {
            m_upgradesList.ForEach((UpgradeComponentData component) =>
            {
                component.ApplyUpgrade();
            });
        }
        public string Name { get { return LocalisationManager.Instance.GetText(m_nameLocalisationID); } }
        public string Description {
            get
            {
                string text = "";
                for(int i = 0; i < m_upgradesList.Count; i++)
                {
                    text += m_upgradesList[i].Description;
                    if(i < m_upgradesList.Count - 1) { text += "\n"; }
                }
                return text;
            }
        }
        public Sprite Icon { get { return m_icon; } }
    }
}
