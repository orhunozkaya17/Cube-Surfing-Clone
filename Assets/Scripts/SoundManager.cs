using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private GameObject audioSourcePrefab;
    [Header("Stack")]
    public AudioClip stackSound;   
    [Header("Gem")]
    public AudioClip gemSound;   
    [Header("Obstacle")]
    public AudioClip obstacleSound; 
    [Header("Speed Pad")]
    public AudioClip speedSound;

    void Awake()
    {
        if (instance == null)
            instance = this;

    }

    public void PlayGemSound(Vector3 pos)
    {
        GameObject go = ObjectPool.instance.ReuseObject(audioSourcePrefab, pos, Quaternion.identity);
        go.SetActive(true);
        go.GetComponent<SoundPrefab>().Init(gemSound);
    }
    public void PlayStackCollect(Vector3 pos)
    {
        GameObject go = ObjectPool.instance.ReuseObject(audioSourcePrefab, pos, Quaternion.identity);
        go.SetActive(true);
        go.GetComponent<SoundPrefab>().Init(stackSound);
    }   public void PlayObstacle(Vector3 pos)
    {
        GameObject go = ObjectPool.instance.ReuseObject(audioSourcePrefab, pos, Quaternion.identity);
        go.SetActive(true);
        go.GetComponent<SoundPrefab>().Init(obstacleSound);
    } 
    public void PlaySpeedSound(Vector3 pos)
    {
        GameObject go = ObjectPool.instance.ReuseObject(audioSourcePrefab, pos, Quaternion.identity);
        go.SetActive(true);
        go.GetComponent<SoundPrefab>().Init(speedSound);
    }
}
