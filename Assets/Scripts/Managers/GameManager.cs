using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] GameData m_gameData;

        public GameData GameData { get { return m_gameData; } }
    }
}