using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] GameData m_gameData;
        [SerializeField] GUIData m_GUIData;

        public GameData GameData { get { return m_gameData; } }
        public GUIData GUIData { get { return m_GUIData; } }
    }
}