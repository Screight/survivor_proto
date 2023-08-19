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
        float m_growSpeed;

        private void Awake()
        {
            m_damageTMP = GetComponent<TMPro.TextMeshProUGUI>();
            m_timer = new Timer(m_timeToDisappear, false, false, OnTick, Disable, false);
        }

        public void OnTick(float p_deltaTime)
        {
            m_damageTMP.transform.localScale += Time.deltaTime * m_growSpeed * Vector3.one;
        }

        private void OnEnable() {
            m_timer?.Restart();
        }
        private void OnDisable() { m_timer?.Stop(); }

        private void Disable()
        {
            LevelManager.Instance.SplashTextPool.AddObject(gameObject);
        }

        public void SetUp(Vector3 p_pos, int p_value, float p_duration, float p_maxSize)
        {
            m_damageTMP.transform.localScale = Vector3.one;
            m_growSpeed = (p_maxSize - 1) / p_duration;
            m_timer.Period = p_duration;
            m_damageTMP.text = p_value.ToString();
            transform.position = p_pos;
        }
    }
}