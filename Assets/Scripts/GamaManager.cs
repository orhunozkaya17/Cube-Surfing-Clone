using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance;

    public GameState gameState;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        Events.GameOver += Events_GameOver;
        Events.WinGame += Events_WinGame;
    }

    private void Events_WinGame()
    {
        gameState = GameState.winLine;
    }

    private void Events_GameOver()
    {
        gameState = GameState.GameOver;
    }

    private void OnDisable()
    {
        Events.GameOver -= Events_GameOver;
    }

}


