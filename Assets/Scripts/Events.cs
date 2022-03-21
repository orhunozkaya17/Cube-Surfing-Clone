using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Events
{

    public static event Action<GameObject> ObstacleHit;
    public static event Action GameOver;

    public static event Action<GemsArg> GemCollect;
    public static event Action<int> StackCollect;

    public static event Action WinGame;

    public static void CallWinGame()
    {
        WinGame?.Invoke();
    }
    public static void CallStackCollectEvent(int stack)
    {
        StackCollect?.Invoke(stack);
    }
    public static void CallObstacleHitEvent(GameObject obstacle)
    {
        ObstacleHit?.Invoke(obstacle);
    }
    public static void CallGameOverEvent()
    {
        GameOver?.Invoke();
    }
    public static void CallScoreChangedEvent(GemsArg arg)
    {
        GemCollect?.Invoke(arg);
    }

}
public class GemsArg : EventArgs
{
    public GameObject gem;
    public Vector3 pos;
}
