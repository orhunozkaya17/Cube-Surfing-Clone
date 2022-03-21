using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemmultipier : MonoBehaviour
{
    public int gemmultipier = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            GamaManager.Instance.gemMultipier = gemmultipier;
        }
    }
}
