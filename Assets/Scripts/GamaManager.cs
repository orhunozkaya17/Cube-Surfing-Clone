using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance;

    [HideInInspector] public GameState gameState=GameState.None;
    [HideInInspector] public int gemMultipier = 0;
    private void Awake()
    {
        Instance = this;
        gameState = GameState.None;
    }
    private void OnEnable()
    {
        Events.GameOver += Events_GameOver;
        Events.WinGame += Events_WinGame;
        Events.GameStart += Events_GameStart;
    }
    private void OnDisable()
    {
        Events.GameOver -= Events_GameOver;
        Events.WinGame -= Events_WinGame;
        Events.GameStart -= Events_GameStart;
    }

    private void Events_GameStart()
    {
        gameState = GameState.Playing;
    }

    private void Events_WinGame()
    {
        gameState = GameState.winLine;
    }

    private void Events_GameOver()
    {
        gameState = GameState.GameOver;
    }



}


