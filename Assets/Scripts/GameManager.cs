using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int score;
    private float Timmer;
    private bool isTimmerActive;


    private void Start()
    {
        Lander.Instance.OnCoinPickUp += Lander_OnpickUpCoin;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChange += Lander_Instance;
    }

    private void Lander_Instance(object sender, Lander.OnStateChangeEventArgs e)
    {
        isTimmerActive = e._state == Lander.State.Normal;
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
}
