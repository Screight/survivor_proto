using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_GUIData", menuName = "SurvivorProto/GUIData", order = 1)]
    public class GUIData : ScriptableObject
    {
        [SerializeField] float m_damageSplashTextInsideRadius;
        [SerializeField] float m_damageSplashTextOutsideRadius;

        [Header("Health")]
        [SerializeField] Sprite m_emptyHealthSprite;
        [SerializeField] Sprite m_fullHealthSprite;

        [Header("Upgrades")]
        [SerializeField] float m_upgradeIconScaleOnMouseOver;

        #region Accessors
        public float DamageSplashTextInsideRadius { get { return m_damageSplashTextInsideRadius; } }
        public float DamageSplashTextOutsideRadius { get { return m_damageSplashTextOutsideRadius; } }
        public Sprite EmptyHealthSprite { get { return m_emptyHealthSprite; } }
        public Sprite FullHealthSprite { get { return m_fullHealthSprite; } }
        public float UpgradeIconScaleOnMouseOver
        {
            get { return m_upgradeIconScaleOnMouseOver; }
            #endregion
        }
    }
}
