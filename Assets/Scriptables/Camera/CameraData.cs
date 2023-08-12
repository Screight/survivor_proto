using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivorProto
{
    [CreateAssetMenu(fileName = "_CameraData", menuName = "SurvivorProto/CameraData", order = 1)]
    public class CameraData : ScriptableObject
    {
        [SerializeField] float m_smoothing;
        [SerializeField] float m_lookAhead;

        public float Smoothing { get { return m_smoothing; } }
        public float LookAhead { get { return m_lookAhead; } }
    }
}
