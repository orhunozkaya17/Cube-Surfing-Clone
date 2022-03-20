using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float time = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            
        {
           
            other.GetComponent<Player>().SpeedUpdate(speed, time);
        }
    }
}
