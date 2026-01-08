using NUnit.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private static int totalScore = 0;
    private int score;
    private float Timmer;
    private bool isTimmerActive;

    private static int CurrentlevelNumber = 1;

    public static void ResetData()
    {
        CurrentlevelNumber = 1;
        totalScore = 0;
    }

    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnPause;

    [SerializeField] private List<GameLevel> gameLevelList;

    [SerializeField] private CinemachineCamera cinemachineCamera;


    private void Start()
    {
        Lander.Instance.OnCoinPickUp += Lander_OnpickUpCoin;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChange += Lander_Instance;

        GameInput.instance.OnMenuButtonPressd += GameInput_OnMeButtonPressed;

        LoadCurrentLevel();
    }

    private void GameInput_OnMeButtonPressed(object sender, EventArgs e)
    {
        PauseOrContinute();
    }

    private void Lander_Instance(object sender, Lander.OnStateChangeEventArgs e)
    {
        isTimmerActive = e._state == Lander.State.Normal;

        if (e._state == Lander.State.Normal)
        {
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom.instance.SetNormalCam();
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isTimmerActive)
            Timmer += Time.deltaTime;
    }

    private void LoadCurrentLevel()
    {
        GameLevel gameLevel = getGameLevel();
        GameLevel spawnGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
        Lander.Instance.transform.position = spawnGameLevel.GetLanderStartPoint();
        cinemachineCamera.Target.TrackingTarget = spawnGameLevel.GetCameraStartTaget();
        CinemachineCameraZoom.instance.SettagetOrthographicSize(spawnGameLevel.GetZoomLevel());
    }

    private GameLevel getGameLevel()
    {
        foreach (var item in gameLevelList)
        {
            if (item.GetLevelNUmber() == CurrentlevelNumber)
            {
                return item;
            }
        }
        return null;
    }

    public float GetTimmer()
    {
        return Timmer;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEvenArgs e)
    {
        AddScore(e._score);
    }

    private void Lander_OnpickUpCoin(object sender, EventArgs e)
    {
        AddScore(500);
        Debug.Log($"Got the Coin");
    }

    public void AddScore(int addAcoreAmount)
    {
        score += addAcoreAmount;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLevel()
    {
        return CurrentlevelNumber;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public void GotoNextLevel()
    {
        totalScore += score;
        CurrentlevelNumber++;

        if (getGameLevel() == null)
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
        }


    }

    public void ReTryLevel()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene); ;
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0f;
        OnGamePause?.Invoke(this, EventArgs.Empty);
    }

    public void ContinuteTheGame()
    {
        Time.timeScale = 1f;
        OnGameUnPause?.Invoke(this, EventArgs.Empty);
    }

    public void PauseOrContinute()
    {
        if (Time.timeScale == 1f)
        {
            PauseTheGame();
        }
        else
        {
            ContinuteTheGame();
        }
    }

}
