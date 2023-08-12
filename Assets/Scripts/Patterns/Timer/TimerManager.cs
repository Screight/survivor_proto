using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    List<Timer> m_timerList = new List<Timer>();

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        m_timerList.ForEach((Timer timer) =>
        {
            timer.Tick(deltaTime);
        });
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
