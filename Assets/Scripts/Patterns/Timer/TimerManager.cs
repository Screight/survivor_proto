using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    List<Timer> m_timerList = new List<Timer>();

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        for (int i = 0; i < m_timerList.Count;)
        {
            Timer timer = m_timerList[i];
            if (timer.Tick(deltaTime)) { i++; }
        }
    }

    public bool AddTimer(Timer p_timer)
    {
        if (m_timerList.Contains(p_timer)) { return false; }
        m_timerList.Add(p_timer);
        return true;
    }

    public bool RemoveTimer(Timer p_timer)
    {
        return m_timerList.Remove(p_timer);
    }

}
