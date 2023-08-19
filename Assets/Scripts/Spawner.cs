using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] float m_spawnDistance;
        [SerializeField] SpawnLevelData m_spawnLevelData;

        int m_level;

        Timer m_spawnEnemiesTimer;
        Timer m_waveDurationTimer;

        Dictionary<ENEMY_TYPE, ObjectPool> m_enemyPool;

        private void Start()
        {
            m_level = 0;
            m_spawnEnemiesTimer = new Timer(1/(float)m_spawnLevelData.WaveDataList[m_level].Frecuency, false, true, null, SpawnEnemies, true);
            m_waveDurationTimer = new Timer(m_spawnLevelData.WaveDataList[m_level].Duration, false, true, null, ChangeToNextWave, true);

            m_enemyPool = new Dictionary<ENEMY_TYPE, ObjectPool>();
            // TODO:
            //  ONLY CREATE NEW POOLS IF THE ENEMY BEHAVIOUR IS REALLLY DIFFERENT
            //  IF NOT GET THE ENEMY FROM THE VERY SAME POOL AND CHANGE THE SPRITE AND STATS

            SpawnEnemies();
        }

        private void OnDestroy()
        {
            if (!TimerManager.IsNull)
            {
                TimerManager.Instance?.RemoveTimer(m_spawnEnemiesTimer);
                TimerManager.Instance?.RemoveTimer(m_waveDurationTimer);
            }
        }

        Enemy GetEnemy(ENEMY_TYPE p_data)
        {
            if (m_enemyPool.ContainsKey(p_data)) {
                return m_enemyPool[p_data].GetObject().GetComponent<Enemy>();
            }

            m_enemyPool.Add(p_data, new ObjectPool(50, 300, 50, GameManager.Instance.GameData.GetEnemyTypePrefab(p_data)));
            return m_enemyPool[p_data].GetObject().GetComponent<Enemy>();
        }

        public void ReturnEnemy(Enemy p_enemy)
        {
            ENEMY_TYPE enemyType = p_enemy.Data.Type;
            if (m_enemyPool.ContainsKey(enemyType)) { m_enemyPool[enemyType].AddObject(p_enemy.gameObject); }
        }
        void SpawnEnemies()
        {
            // Select a random enemy from a list taking into account their weights
            EnemyData enemyData = null;

            float addedChance = 0;
            float randomNumber = Random.Range(0.0f, 1.0f);

            List<EnemyWaveInfo> enemyWaveInfoList = m_spawnLevelData.WaveDataList[m_level].WaveInfoList;

            foreach (EnemyWaveInfo enemyWaveInfo in enemyWaveInfoList)
            {
                addedChance += enemyWaveInfo.Chance;

                if (randomNumber <= addedChance) {
                    enemyData = enemyWaveInfo.EnemyData;
                    break;
                }
            }

            Enemy enemy = GetEnemy(enemyData.Type);
            enemy.Initialize(enemyData);
            enemy.transform.position = GetRandomPosition();
        }

        void ChangeToNextWave()
        {
            WaveData data = m_spawnLevelData.WaveDataList[m_level];
            if(data == null) { return; }
            m_level++;
            m_spawnEnemiesTimer.Period = 1 / data.Frecuency;
            m_waveDurationTimer.Period = data.Duration;
        }

        Vector2 GetRandomPosition()
        {
            Camera camera = Camera.main;
            Vector2 bottomLeftCameraCorner = camera.ScreenToWorldPoint(Vector2.zero);
            Vector2 topRightCameraCorner = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            float screenWidthInWorldSize = (topRightCameraCorner - bottomLeftCameraCorner).x;
            float screenHeightInWorldSize = (topRightCameraCorner - bottomLeftCameraCorner).y;

            Vector2 cameraSize = new Vector2(screenWidthInWorldSize, screenHeightInWorldSize);
            Vector2 spawnSize = cameraSize + Vector2.one * m_spawnDistance;

            return RandomPointBetween2Rectangles.GetRandomPoint(PlayerController.Instance.transform.position, cameraSize, spawnSize);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Camera camera = Camera.main;
            Vector2 bottomLeftCameraCorner = camera.ScreenToWorldPoint(Vector2.zero);
            Vector2 topRightCameraCorner = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            float screenWidthInWorldSize = (topRightCameraCorner - bottomLeftCameraCorner).x;
            float screenHeightInWorldSize = (topRightCameraCorner - bottomLeftCameraCorner).y;

            Vector2 cameraSize = new Vector2(screenWidthInWorldSize, screenHeightInWorldSize);
            Vector2 spawnSize = cameraSize + Vector2.one * m_spawnDistance;

            Gizmos.DrawWireCube(GameObject.FindObjectOfType<PlayerController>().transform.position, cameraSize);
            Gizmos.DrawWireCube(GameObject.FindObjectOfType<PlayerController>().transform.position, spawnSize);
        }
#endif
    }
}