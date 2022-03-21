using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAdd : MonoBehaviour
{
    [SerializeField] private int countstack = 1;
    [SerializeField] private GameObject stackPrefab;


    private void Awake()
    {
        if (countstack >= 2)
        {

            for (int i = 1; i < countstack; i++)
            {
                GameObject go = Instantiate(stackPrefab, transform);
                go.transform.localPosition = new Vector3(0, i, 0);
            }
        }
        else
        {
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Events.CallStackCollectEvent(countstack);
        }
    }
}
