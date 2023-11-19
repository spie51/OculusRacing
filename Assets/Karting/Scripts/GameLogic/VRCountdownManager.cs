using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRCountdownManager : MonoBehaviour
{
    public float TimeRemaining { get; private set; }
    public bool IsOver { get; private set; }
    public TextMeshProUGUI timerText;

    private bool countdownStarted;

    void Update()
    {
        // Debug.Log("Outside" + TimeElapsed);

        if (!countdownStarted) return;

        if (!IsOver)
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                IsOver = true;
            }
            // Debug.Log(TimeRemaining);
            timerText.text = "" + (int)Math.Ceiling(TimeRemaining);

            // TimeElapsed += Time.deltaTime;
            // Debug.Log("Inside" + TimeElapsed);
        }
    }

    public void StartCountdown(float countdownTime)
    {
        countdownStarted = true;
        TimeRemaining = countdownTime;
    }

    public void StopCountdown()
    {
        countdownStarted = false;
    }
}

