using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalEffectController : MonoBehaviour
{




    [SerializeField] private GameObject gem;
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
}
