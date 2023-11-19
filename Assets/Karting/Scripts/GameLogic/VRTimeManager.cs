using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRTimeManager : MonoBehaviour
{
    public bool IsFinite { get; private set; }
    public float TotalTime { get; private set; }
    public float TimeRemaining { get; private set; }
    public float TimeElapsed { get; private set; }
    public bool IsOver { get; private set; }
    public TextMeshProUGUI timerText;

    private bool raceStarted;

    public static Action<float> OnAdjustTime;
    public static Action<int, bool, GameMode> OnSetTime;

    private void Awake()
    {
        IsFinite = false;
        TimeRemaining = TotalTime;
    }


    void OnEnable()
    {
        OnAdjustTime += AdjustTime;
        OnSetTime += SetTime;
    }

    private void OnDisable()
    {
        OnAdjustTime -= AdjustTime;
        OnSetTime -= SetTime;
    }

    private void AdjustTime(float delta)
    {
        TimeRemaining += delta;
    }

    private void SetTime(int time, bool isFinite, GameMode gameMode)
    {
        TotalTime = time;
        IsFinite = isFinite;
        TimeRemaining = TotalTime;
    }

    void Update()
    {
        // Debug.Log("Outside" + TimeElapsed);

        if (!raceStarted) return;

        if (!IsOver)
        {
            // TimeRemaining -= Time.deltaTime;
            // if (TimeRemaining <= 0)
            // {
            //     TimeRemaining = 0;
            //     IsOver = true;
            // }

            TimeElapsed += Time.deltaTime;

            timerText.gameObject.SetActive(true);
            int timeRemaining = (int)Math.Floor(TimeElapsed);
            int deciRemaining = (int)Math.Floor(TimeElapsed * 10) % 10;
            string minuteText = timeRemaining / 60 > 9 ? "" + timeRemaining / 60 : "0" + timeRemaining / 60;
            string secondText = timeRemaining % 60 > 9 ? "" + timeRemaining % 60 : "0" + timeRemaining % 60;
            timerText.text = minuteText + ":" + secondText + ":" + deciRemaining;

            // Debug.Log(minuteText + ":" + secondText + ":" + deciRemaining);

            // Debug.Log("Inside" + TimeElapsed);
        }
    }

    public void StartRace()
    {
        raceStarted = true;
        TimeElapsed = 0.0f;
    }

    public void StopRace()
    {
        raceStarted = false;
    }
}

