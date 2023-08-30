using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public abstract class UpgradeComponentData : ScriptableObject
    {
        [SerializeField] uint m_descriptionLocalisationID;
        public abstract void ApplyUpgrade();
        public string Description { get { return ParsedDescription(LocalisationManager.Instance.GetText(m_descriptionLocalisationID)); } }
        protected abstract string ParsedDescription(string p_description);
    }
}