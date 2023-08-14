using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_UpgradeData", menuName = "SurvivorProto/Upgrade/UpgradeData", order = 1)]
    public class UpgradeData : ScriptableObject
    {
        [SerializeField] Sprite m_icon;
        [SerializeField] string m_name;

        [SerializeField] List<UpgradeComponentData> m_upgradesList;

        public void OnCollectUpgrade()
        {
            m_upgradesList.ForEach((UpgradeComponentData component) =>
            {
                component.ApplyUpgrade();
            });
        }
        public string Name { get { return m_name; } }
        public string Description {
            get
            {
                string text = "";
                for(int i = 0; i < m_upgradesList.Count; i++)
                {
                    text += m_upgradesList[i].ParsedDescription();
                    if(i < m_upgradesList.Count - 1) { text += "\n"; }
                }
                return text;
            }
        }
        public Sprite Icon { get { return m_icon; } }
    }
}
