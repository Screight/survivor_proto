using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class SplashText : MonoBehaviour
    {
        [SerializeField] float m_timeToDisappear;
        Timer m_timer;

        TMPro.TextMeshProUGUI m_damageTMP;

        private void Awake()
        {
            m_timer = new Timer(m_timeToDisappear, false, false, null, Disable, false);
            m_damageTMP = GetComponent<TMPro.TextMeshProUGUI>();
        }

        private void OnEnable() {
            m_timer.Restart();
        }
        private void OnDisable() { m_timer.Stop(); }

        private void Disable()
        {
            LevelManager.Instance.SplashTextPool.AddObject(gameObject);
        }

        public void SetUp(Vector3 p_pos, int p_value)
        {
            m_damageTMP.text = p_value.ToString();
            transform.position = p_pos;
        }
    }
}