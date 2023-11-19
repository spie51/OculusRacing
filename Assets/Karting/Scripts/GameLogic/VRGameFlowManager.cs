using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using KartGame.KartSystems;
using UnityEngine.SceneManagement;
using TMPro;

// public enum GameState{Play, Won, Lost}

public class VRGameFlowManager : MonoBehaviour
{
    // [Header("Parameters")]
    // [Tooltip("Duration of the fade-to-black at the end of the game")]
    // public float endSceneLoadDelay = 3f;
    // [Tooltip("The canvas group of the fade-to-black screen")]
    // public CanvasGroup endGameFadeCanvasGroup;

    // [Header("Win")]
    // [Tooltip("This string has to be the name of the scene you want to load when winning")]
    // public string winSceneName = "WinScene";
    // [Tooltip("Duration of delay before the fade-to-black, if winning")]
    // public float delayBeforeFadeToBlack = 4f;
    // [Tooltip("Duration of delay before the win message")]
    // public float delayBeforeWinMessage = 2f;
    // [Tooltip("Sound played on win")]
    // public AudioClip victorySound;

    // [Tooltip("Prefab for the win game message")]
    // public DisplayMessage winDisplayMessage;

    // public PlayableDirector raceCountdownTrigger;

    // [Header("Lose")]
    // [Tooltip("This string has to be the name of the scene you want to load when losing")]
    // public string loseSceneName = "LoseScene";
    // [Tooltip("Prefab for the lose game message")]
    // public DisplayMessage loseDisplayMessage;


    // public GameState gameState { get; private set; }

    public bool autoFindKarts = true;
    public ArcadeKart playerKart;

    ArcadeKart[] karts;
    // ObjectiveManager m_ObjectiveManager;
    // public TextMeshProUGUI timerText;
    VRTimeManager m_TimeManager;
    VRCountdownManager m_CountdownManager;
    public FinishLineObject finishLineObject;
    // float m_TimeLoadEndGameScene;
    // string m_SceneToLoad;
    // float elapsedTimeBeforeEndScene = 0;
    bool gameConcluded = false;

    void Start()
    {
        if (autoFindKarts)
        {
            karts = FindObjectsOfType<ArcadeKart>();
            if (karts.Length > 0)
            {
                if (!playerKart) playerKart = karts[0];
            }
            DebugUtility.HandleErrorIfNullFindObject<ArcadeKart, VRGameFlowManager>(playerKart, this);
        }

        // m_ObjectiveManager = FindObjectOfType<ObjectiveManager>();
        // DebugUtility.HandleErrorIfNullFindObject<ObjectiveManager, GameFlowManager>(m_ObjectiveManager, this);
        finishLineObject = FindObjectOfType<FinishLineObject>();
        DebugUtility.HandleErrorIfNullFindObject<FinishLineObject, VRGameFlowManager>(finishLineObject, this);

        m_TimeManager = FindObjectOfType<VRTimeManager>();
        DebugUtility.HandleErrorIfNullFindObject<VRTimeManager, VRGameFlowManager>(m_TimeManager, this);

        m_CountdownManager = FindObjectOfType<VRCountdownManager>();
        DebugUtility.HandleErrorIfNullFindObject<VRCountdownManager, VRGameFlowManager>(m_CountdownManager, this);

        // AudioUtility.SetMasterVolume(1);

        // winDisplayMessage.gameObject.SetActive(false);
        // loseDisplayMessage.gameObject.SetActive(false);

        m_TimeManager.StopRace();
        foreach (ArcadeKart k in karts)
        {
            k.SetCanMove(false);
        }

        //run race countdown animation
        StartCountdown();
        // StartCoroutine(ShowObjectivesRoutine());

        StartCoroutine(CountdownThenStartRaceRoutine());
    }

    IEnumerator CountdownThenStartRaceRoutine()
    {
        yield return new WaitForSeconds(3f);
        StartRace();
    }

    void StartRace()
    {
        foreach (ArcadeKart k in karts)
        {
            k.SetCanMove(true);
        }
        m_TimeManager.StartRace();
    }
    void StartCountdown()
    {
        // raceCountdownTrigger.Play();
        m_CountdownManager.StartCountdown(3.0f);
    }

    // IEnumerator ShowObjectivesRoutine() {
    //     while (m_ObjectiveManager.Objectives.Count == 0)
    //         yield return null;
    //     yield return new WaitForSecondsRealtime(0.2f);
    //     for (int i = 0; i < m_ObjectiveManager.Objectives.Count; i++)
    //     {
    //        if (m_ObjectiveManager.Objectives[i].displayMessage)m_ObjectiveManager.Objectives[i].displayMessage.Display();
    //        yield return new WaitForSecondsRealtime(1f);
    //     }
    // }

    void EndGame(bool winner)
    {
        Debug.Log(winner ? "You won!" : "You lost!");
        SceneManager.LoadScene("EndScene");
        // AudioUtility.SetMasterVolume(0.5f);
        // StartCoroutine(LoadScene(winner));
    }

    IEnumerator LoadScene(bool winner)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("EndScene");
    }


    void Update()
    {

        if (!gameConcluded)
        {
            int gameModifier = -1;
            if (finishLineObject.playerFinished && !finishLineObject.AIFinished)
            {
                gameConcluded = true;
                gameModifier = 1;
            }
            else if (!finishLineObject.playerFinished && finishLineObject.AIFinished)
            {
                gameConcluded = true;
                gameModifier = 0;
            }
            if (gameModifier != -1)
            {
                bool winner = gameModifier == 1 ? true : false;
                EndGame(winner);
            }
        }
        // if (gameState != GameState.Play)
        // {
        //     elapsedTimeBeforeEndScene += Time.deltaTime;
        //     if(elapsedTimeBeforeEndScene >= endSceneLoadDelay)
        //     {

        //         float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / endSceneLoadDelay;
        //         endGameFadeCanvasGroup.alpha = timeRatio;

        //         float volumeRatio = Mathf.Abs(timeRatio);
        //         float volume = Mathf.Clamp(1 - volumeRatio, 0, 1);
        //         AudioUtility.SetMasterVolume(volume);

        //         // See if it's time to load the end scene (after the delay)
        //         if (Time.time >= m_TimeLoadEndGameScene)
        //         {
        //             SceneManager.LoadScene(m_SceneToLoad);
        //             gameState = GameState.Play;
        //         }
        //     }
        // }
        // else
        // {
        //     if (m_ObjectiveManager.AreAllObjectivesCompleted())
        //         EndGame(true);

        //     if (m_TimeManager.IsFinite && m_TimeManager.IsOver)
        //         EndGame(false);
        // }
    }

    // void EndGame(bool win)
    // {
    //     // unlocks the cursor before leaving the scene, to be able to click buttons
    //     Cursor.lockState = CursorLockMode.None;
    //     Cursor.visible = true;

    //     m_TimeManager.StopRace();

    //     // Remember that we need to load the appropriate end scene after a delay
    //     gameState = win ? GameState.Won : GameState.Lost;
    //     endGameFadeCanvasGroup.gameObject.SetActive(true);
    //     if (win)
    //     {
    //         m_SceneToLoad = winSceneName;
    //         m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

    //         // play a sound on win
    //         var audioSource = gameObject.AddComponent<AudioSource>();
    //         audioSource.clip = victorySound;
    //         audioSource.playOnAwake = false;
    //         audioSource.outputAudioMixerGroup = AudioUtility.GetAudioGroup(AudioUtility.AudioGroups.HUDVictory);
    //         audioSource.PlayScheduled(AudioSettings.dspTime + delayBeforeWinMessage);

    //         // create a game message
    //         winDisplayMessage.delayBeforeShowing = delayBeforeWinMessage;
    //         winDisplayMessage.gameObject.SetActive(true);
    //     }
    //     else
    //     {
    //         m_SceneToLoad = loseSceneName;
    //         m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

    //         // create a game message
    //         loseDisplayMessage.delayBeforeShowing = delayBeforeWinMessage;
    //         loseDisplayMessage.gameObject.SetActive(true);
    //     }
    // }
}

