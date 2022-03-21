using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalEffectController : MonoBehaviour
{
    public static ParticalEffectController Instance;

    
    
    [SerializeField] private GameObject gem;
    [SerializeField] private GameObject stackEffect;
    [SerializeField] private ParticleSystem speedEffect;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        Events.GemCollect += OnGemCollect;
    }
    private void OnDisable()
    {
        Events.GemCollect -= OnGemCollect;
    }
    private void OnGemCollect(GemsArg obj)
    {
        GameObject Gem = ObjectPool.instance.ReuseObject(gem, obj.pos + Vector3.up,Quaternion.Euler(-90,0,0));
        Gem.SetActive(true);
    }
    public void stackCollectEffect(Vector3 pos)
    {
        GameObject stackcollect = ObjectPool.instance.ReuseObject(stackEffect, pos, Quaternion.Euler(-90, 0, 0));
        stackcollect.SetActive(true);
    }

    public  ParticleSystem SpeedEffect()
    {
        return speedEffect;
    }
}
