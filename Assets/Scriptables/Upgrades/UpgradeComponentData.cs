using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public abstract class UpgradeComponentData : ScriptableObject
    {
        [TextArea(5, 5)]
        [SerializeField] protected string m_description;

        public abstract void ApplyUpgrade();
        protected string Description { get { return m_description; } }
        public abstract string ParsedDescription();
    }
}