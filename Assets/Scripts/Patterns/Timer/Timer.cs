using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTickDelegate(float p_deltaTime);
public delegate void OnFinishedDelegate();

public class Timer
{
    float m_currentTime;
    float m_period;

    bool m_destroyOnFinished;
    bool m_repeatOnFinished;

    event OnTickDelegate OnTickEvent;
    event OnFinishedDelegate OnFinishedEvent;

    bool m_isPaused;

    public Timer(float p_period, bool p_destroyOnFinished, bool p_repeatOnFinished, OnTickDelegate p_onTickDelegate, OnFinishedDelegate p_onFinishedEvent, bool p_autoStart)
    {
        m_period = p_period;
        m_destroyOnFinished = p_destroyOnFinished;
        m_repeatOnFinished = p_repeatOnFinished;

        m_isPaused = !p_autoStart;
        if (p_autoStart) { m_currentTime = 0; }

        OnTickEvent += p_onTickDelegate;
        OnFinishedEvent += p_onFinishedEvent;

        TimerManager.Instance.AddTimer(this);
    }

    public void Tick(float p_deltaTime)
    {
        if(m_currentTime == -1) { return; }
        if (m_isPaused) { return; }

        m_currentTime += p_deltaTime;
        OnTickEvent?.Invoke(p_deltaTime);

        if (m_currentTime >= m_period)
        {
            OnFinishedEvent?.Invoke();
            if (m_destroyOnFinished) {
                OnTickEvent = null;
                OnFinishedEvent = null;
                TimerManager.Instance.RemoveTimer(this);
            }
            else if (m_repeatOnFinished) { m_currentTime = 0; }
        }
    }

    public bool IsPaused { get { return m_isPaused && m_currentTime >=0; } }
    public bool IsRunning { get { return !m_isPaused && m_currentTime >= 0; } }

    public void AddOnTickEvent(OnTickDelegate p_onTickDelegate) { OnTickEvent += p_onTickDelegate; }
    public void AddOnFinishedEvent(OnFinishedDelegate p_onFinishedDelegate)
    {
        OnFinishedEvent += p_onFinishedDelegate;
    }

    public void ClearEvents() { OnTickEvent = null; OnFinishedEvent = null; }
    public float Period { get { return m_period; } set { m_period = value; } }
    public float CurrentTime { get { return m_currentTime; } }
    public void Restart() { m_currentTime = 0; m_isPaused = false; }
    public void Pause() { m_isPaused = true; }
    public void Stop() { m_currentTime = -1; m_isPaused = true; }
}
