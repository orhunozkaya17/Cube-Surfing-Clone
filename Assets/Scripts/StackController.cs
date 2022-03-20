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
    }

    private void ObstacleHit(GameObject obj)
    {
        stacks.Remove(obj.transform);
        if (stacks.Count == 0)
        {
            Events.CallGameOverEvent();
        }
    }

    private void OnDisable()
    {
        Events.ObstacleHit -= ObstacleHit;
    }
    void AddStack(int stackCount)
    {
        for (int i = 0; i < stackCount; i++)
        {
            GameObject stack = Instantiate(stackPrefab, stackParent);
            stack.transform.localPosition = new Vector3(0, stacks[stacks.Count - 1].localPosition.y + 1f, 0);
            stacks.Add(stack.transform);
        }

        Player.localPosition = new Vector3(0, stacks[stacks.Count - 1].position.y + 1f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stack")
        {
            AddStack(1);
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Gem"))
        {
            Events.CallScoreChangedEvent(new GemsArg() { pos = transform.position, gem=other.gameObject });
            other.gameObject.SetActive(false);
        }
    }
}
