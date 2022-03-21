using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] GameObject stackPrefab;
    [SerializeField] Transform stackParent;
    [SerializeField] Transform Player;

    List<Transform> stacks = new List<Transform>();
    bool winGame = false;
  
    void Start()
    {
        foreach (Transform child in stackParent)
        {
            stacks.Add(child);
        }

    }
    private void OnEnable()
    {
        Events.ObstacleHit += ObstacleHit;
        Events.StackCollect += Events_StackCollect;
    }
    private void OnDisable()
    {
        Events.ObstacleHit -= ObstacleHit;
        Events.StackCollect -= Events_StackCollect;
    }
    private void Events_StackCollect(int stackAdd)
    {
        AddStack(stackAdd);
    }

    private void ObstacleHit(GameObject obj)
    {
        stacks.Remove(obj.transform);
        Destroy(obj,4f);
        if (winGame && stacks.Count == 0)
        {
            Events.CallComplateGame();
        }else if (stacks.Count == 0)
        {
            Events.CallGameOverEvent();
        }
    }

   
    void AddStack(int stackCount)
    {
        for (int i = 0; i < stackCount; i++)
        {
            GameObject stack = Instantiate(stackPrefab, stackParent);
            stack.transform.localPosition = new Vector3(0, stacks[stacks.Count - 1].localPosition.y + 1f, 0);
            stacks.Add(stack.transform);
        }
        SoundManager.instance.PlayStackCollect(transform.position);
        Player.localPosition = new Vector3(0, stacks[stacks.Count - 1].position.y + 1f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stack")
        {
            ParticalEffectController.Instance.stackCollectEffect(other.transform.position);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("FinishGame"))
        {
            winGame = true;
            Events.CallWinGame();
           
        }
   
    }
}
