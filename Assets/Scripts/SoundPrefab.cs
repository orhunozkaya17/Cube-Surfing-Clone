using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPrefab : MonoBehaviour
{
   AudioSource source;
    float disabletime;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void Init(AudioClip clip) 
    {
        StopAllCoroutines();
        source.clip = clip;
        source.pitch = Random.Range(0.9f, 1.1f);
        source.Play();
        disabletime = clip.length;
        StartCoroutine(Disable());
    }
    IEnumerator Disable() {
        yield return new WaitForSeconds(disabletime);
        gameObject.SetActive(false);
    }
        
}
