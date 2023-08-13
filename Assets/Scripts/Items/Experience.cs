using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class Experience : MonoBehaviour, ICollectible
    {
        float m_amount;
        float m_speed;

        bool m_goToPlayer;
        Transform m_playerTr;

        SpriteRenderer m_renderer;

        private void Awake()
        {
            m_renderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            m_playerTr = PlayerController.Instance.transform;
            m_speed = GameManager.Instance.GameData.ExperienceCollectibleSpeed;
        }

        private void Update()
        {
            if (!m_goToPlayer) { return; }
            Vector2 direction = (m_playerTr.position - transform.position).normalized;
            transform.position += (Vector3)(m_speed * direction * Time.deltaTime);
        }
        public void StartAttracting() { m_goToPlayer = true; }
        public float Amount { get { return m_amount; } set { m_amount = value; } }
        public float Speed { get { return m_speed; } set { m_speed = value; } }

        public void Initialize(float p_amount)
        {
            m_amount = p_amount;
            m_goToPlayer = false;
            List<ExperienceCollectible> expList = GameManager.Instance.GameData.ExperienceCollectibleList;

            foreach(ExperienceCollectible collectible in expList)
            {
                if(m_amount <= collectible.MaxValue)
                {
                    m_renderer.sprite = collectible.Sprite;
                    break;
                }
            }
            m_renderer.sprite = expList[expList.Count - 1].Sprite;
        }

        public void OnCollect()
        {
            PlayerController.Instance.Stats.GainExperience(m_amount);
            LevelManager.Instance.ExperienceManager.ExperienceCollectiblePool.AddObject(gameObject);
        }
    }
}