using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeRemaining = 60f;
    public bool timerRunning = false;
    

    void Update()
    {
        if (!timerRunning) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            timerRunning = false;
            UpdateTimerUI(timeRemaining);

            GameManager.Instance.OnTimeUp();

        }
    }

    public void StartTimer(float duration)
    {
        timeRemaining = duration;
        timerRunning = true;
    }

    public void PauseTimer()
    {
        timerRunning = false;
    }

    public void ResumeTimer()
    {
        timerRunning = true;
    }

    public void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
